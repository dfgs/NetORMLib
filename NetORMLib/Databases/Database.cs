using NetORMLib.Columns;
using NetORMLib.CommandBuilders;
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
		private DbConnection connection;
		private ICommandBuilder commandBuilder;

		private DbTransaction transaction;

		public Database(DbConnection Connection, ICommandBuilder CommandBuilder)
		{
			this.connection = Connection;
			this.commandBuilder = CommandBuilder;
		}

		public void BeginTransaction()
		{
			if (transaction != null) throw new InvalidOperationException("A transaction is already running");
			connection.Open();
			transaction = connection.BeginTransaction();
		}
		public void EndTransaction(bool Commit)
		{
			if (transaction == null) throw new InvalidOperationException("There is no running transaction");
			if (Commit) transaction.Commit();
			else transaction.Rollback();
			transaction = null;
			connection.Close();
		}

		public IEnumerable<Row> Execute(ISelect Query)
		{
			DbCommand command;
			DbDataReader reader;
			dynamic row;
			IColumn[] columns;

			columns = Query.Columns.ToArray();

			command = commandBuilder.BuildCommand(Query);
			command.Connection = connection;

			if (transaction == null) connection.Open();
			else command.Transaction = transaction;

			using (reader = command.ExecuteReader())
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
			if (transaction == null) connection.Close();
		}

		public void Execute(IDelete Query)
		{
			DbCommand command;

			command = commandBuilder.BuildCommand(Query);
			command.Connection = connection;

			if (transaction == null) connection.Open();
			else command.Transaction = transaction;

			command.ExecuteNonQuery();

			if (transaction == null) connection.Close();
		}

		public void Execute(IUpdate Query)
		{
			DbCommand command;

			command = commandBuilder.BuildCommand(Query);
			command.Connection = connection;

			if (transaction == null) connection.Open();
			else command.Transaction = transaction;

			command.ExecuteNonQuery();

			if (transaction == null) connection.Close();
		}

	}
}
