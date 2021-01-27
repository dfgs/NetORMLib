using NetORMLib.Columns;
using NetORMLib.CommandBuilders;
using NetORMLib.ConnectionFactories;
using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Databases
{
	public class Database:IDatabase
	{
		private readonly IConnectionFactory connectionFactory;
		private readonly ICommandBuilder commandBuilder;


		public Database(IConnectionFactory ConnectionFactory, ICommandBuilder CommandBuilder)
		{
			this.connectionFactory = ConnectionFactory;
			this.commandBuilder = CommandBuilder;
		}

		public IEnumerable<string> GetTables()
		{
			using (DbConnection connection=connectionFactory.CreateConnection())
			{
				connection.Open();
				foreach (System.Data.DataRow row in connection.GetSchema("Tables").Rows)
				{
					yield return (string)row["TABLE_NAME"];
				}
				connection.Close();
			}
		}

		public IEnumerable<TRow> Execute<TRow>(ISelect Query)
			where TRow : new()
		{
			DbCommand command;
			DbDataReader reader;
			IColumn[] columns;
			TRow row;
			Type type, underlyingType;

			//pdcs=TypeDescriptor.GetProperties(typeof(T));

			columns = Query.Columns.ToArray();
			command = commandBuilder.BuildCommand(Query);

			using (DbConnection connection = connectionFactory.CreateConnection())
			{
				connection.Open();
				command.Connection = connection;
				PropertyDescriptorCollection pdcs;
				object value;

				pdcs = TypeDescriptor.GetProperties(typeof(TRow));
				// ensure that all properties exist
				for (int t = 0; t < columns.Length; t++)
				{
					if (pdcs[columns[t].Name] == null) throw new KeyNotFoundException($"Cannot find property {columns[t].Name} in descriptor collection");
				}

				try
				{
					reader=command.ExecuteReader();
				}
				catch (Exception ex)
				{
					throw new ORMException(command, ex);
				}


				using (reader)
				{
					while (reader.Read())
					{
						row = new TRow();
						for (int t = 0; t < columns.Length; t++)
						{
							value = reader[t];
							// cannot convert DBNull to Nullable<>
							if (value == DBNull.Value)
							{
								value = null;
							}
							else
							{
								type = pdcs[columns[t].Name].PropertyType;
								underlyingType = Nullable.GetUnderlyingType(type);
								if (underlyingType != null) type = underlyingType;

								if (type.IsEnum)
								{
									value = Enum.ToObject(type, value);
								}
							}
							pdcs[columns[t].Name].SetValue(row, value);
						}
						yield return row;
					}
				}
				connection.Close();
			}
		}

		public object Execute(ISelectIdentity Query)
		{
			DbCommand command;
			DbDataReader reader;
			object result = null;

			command = commandBuilder.BuildCommand(Query);

			using (DbConnection connection = connectionFactory.CreateConnection())
			{
				connection.Open();
				command.Connection = connection;

				try
				{
					reader = command.ExecuteReader();
				}
				catch (Exception ex)
				{
					throw new ORMException(command, ex);
				}

				using (reader)
				{
					if (!reader.Read()) throw new ORMException(command, new Exception("No identity value returned"));
					result=reader[0];
					if (Query.ResultCallBack != null) Query.ResultCallBack(result);
				}
			}

			return result;
		}


		public void Execute(IQuery Query)
		{
			DbCommand command;

			command = commandBuilder.BuildCommand(Query);
			using (DbConnection connection = connectionFactory.CreateConnection())
			{
				connection.Open();
				command.Connection = connection;
				try
				{
					command.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					throw new ORMException(command, ex);
				}
			}
		}

		public object Execute(params IQuery[] Queries)
		{
			return Execute((IEnumerable<IQuery>)Queries);
		}

		public object Execute(IEnumerable<IQuery> Queries)
		{
			DbCommand command;
			object result=null;

			using (DbConnection connection = connectionFactory.CreateConnection())
			{
				connection.Open();
				using (DbTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						foreach (IQuery query in Queries)
						{
							command = commandBuilder.BuildCommand(query);
							command.Connection = connection;
							command.Transaction = transaction;
							try
							{
								if (query is ICallBackQuery callBackQuery)
								{
									result=command.ExecuteScalar();
									if (callBackQuery.ResultCallBack!!=null) callBackQuery.ResultCallBack(result);
								}
								else command.ExecuteNonQuery();
							}
							catch(Exception ex)
							{
								throw new ORMException(command, ex);
							}
						}
					}
					catch
					{
						transaction.Rollback();
						throw;
					}
					transaction.Commit();
				}
			}

			return result;
		}




	}
}
