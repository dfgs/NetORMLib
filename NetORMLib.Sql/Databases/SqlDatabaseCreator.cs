using NetORMLib.Databases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql.Databases
{
	public class SqlDatabaseCreator : DatabaseCreator
	{
		private string connectionString;

		public SqlDatabaseCreator(string DatabaseName,string ConnectionString):base(DatabaseName)
		{
			this.connectionString = ConnectionString;
		}

		public override bool DatabaseExists()
		{
			SqlConnection connection;
			SqlCommand command;
			SqlDataReader reader;

			using (connection = new SqlConnection(connectionString))
			{
				connection.Open();
				command = new SqlCommand($"SELECT Name FROM master.dbo.sysdatabases where (Name=@Name)", connection);
				command.Parameters.AddWithValue("@Name", DatabaseName);
				reader = command.ExecuteReader();
				return reader.HasRows;
			}

		}

		public override void CreateDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			connection = new SqlConnection(connectionString);
			connection.Open();
			command = new SqlCommand("CREATE DATABASE TEST", connection);
			connection.Close();
		}
		public override void DropDatabase()
		{
			throw new NotImplementedException();
		}

	}
}
