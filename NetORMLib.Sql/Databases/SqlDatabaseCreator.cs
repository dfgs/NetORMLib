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
		private readonly string server;

		public SqlDatabaseCreator(string Server,string DatabaseName):base(DatabaseName)
		{
			this.server = Server;
		}

		public override bool DatabaseExists()
		{
			SqlConnection connection;
			SqlCommand command;
			SqlDataReader reader;

			using (connection = new SqlConnection($@"Server={server};Trusted_Connection=True;Connect Timeout=30"))
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

			using (connection = new SqlConnection($@"Server={server};Trusted_Connection=True;Connect Timeout=30"))
			{
				connection.Open();
				command = new SqlCommand($"CREATE DATABASE {DatabaseName}", connection);
				command.ExecuteNonQuery();
			}
		}
		public override void DropDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			using (connection = new SqlConnection($@"Server={server};Trusted_Connection=True;Connect Timeout=30"))
			{
				connection.Open();
				command = new SqlCommand($"DROP DATABASE {DatabaseName}", connection);
				command.ExecuteNonQuery();
			}
		}

	}
}
