using NetORMLib.Columns;
using NetORMLib.CommandBuilders;
using NetORMLib.ConnectionFactories;
using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Databases
{
	public class Database:IDatabase
	{
		private IConnectionFactory connectionFactory;
		private ICommandBuilder commandBuilder;

		//private DbTransaction transaction;

		public Database(IConnectionFactory ConnectionFactory, ICommandBuilder CommandBuilder)
		{
			this.connectionFactory = ConnectionFactory;
			this.commandBuilder = CommandBuilder;
		}

		/*public void BeginTransaction()
		{
			if (transaction != null) throw new InvalidOperationException("A transaction is already running");
			connectionFactory.Open();
			transaction = connectionFactory.BeginTransaction();
		}
		public void EndTransaction(bool Commit)
		{
			if (transaction == null) throw new InvalidOperationException("There is no running transaction");
			if (Commit) transaction.Commit();
			else transaction.Rollback();
			transaction = null;
			connectionFactory.Close();
		}*/

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

		public IEnumerable<Row> Execute(ISelect Query)
		{
			DbCommand command;
			DbDataReader reader;
			dynamic row;
			IColumn[] columns;

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
						row = new Row(columns);
						for (int t = 0; t < columns.Length; t++)
						{
							row.TrySetMember(columns[t], reader[t]);
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
				connection.Close();
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
				connection.Close();
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
					catch (Exception ex)
					{
						transaction.Rollback();
						throw (ex);
					}
					transaction.Commit();
				}
				connection.Close();
			}
		}




	}
}
