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

		public IEnumerable<Row<T>> Execute<T>(ISelect Query)
			//where T : new()
		{
			DbCommand command;
			DbDataReader reader;
			IColumn[] columns;
			Row<T> row;
			//PropertyDescriptorCollection pdcs;

			//pdcs=TypeDescriptor.GetProperties(typeof(T));

			columns = Query.Columns.ToArray();
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
					while (reader.Read())
					{
						row = new Row<T>();
						for (int t = 0; t < columns.Length; t++)
						{
							((IRow<T>)row).SetValue(columns[t], reader[t]);
							//pdcs[columns[t].Name].SetValue(row, reader[t]);
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
					result = reader[0];
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

		public void Execute(params IQuery[] Queries)
		{
			Execute((IEnumerable<IQuery>)Queries);
		}

		public void Execute(IEnumerable<IQuery> Queries)
		{
			DbCommand command;
			object result;

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
									callBackQuery?.ResultCallBack(result);
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
		}




	}
}
