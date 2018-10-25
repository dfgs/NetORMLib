using NetORMLib.Databases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql.Databases
{
	public class SqlLocalDatabaseCreator : DatabaseCreator
	{
		private string path;

		public SqlLocalDatabaseCreator(string DatabaseName,string Path):base(DatabaseName)
		{
			this.path = Path;
		}
		//			return new System.Data.SqlClient.SqlConnection();

		private string CreateConnectionString()
		{
			return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.Combine(path, DatabaseName)}.mdf;Integrated Security=True;Connect Timeout=30";
		}
		public override bool DatabaseExists()
		{
			SqlConnection connection;

			try
			{
				using (connection = new SqlConnection(CreateConnectionString()))
				{
					connection.Open();
					connection.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		public override void CreateDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			using (connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB"))
			{
				connection.Open();
				command = new SqlCommand($"CREATE DATABASE [{DatabaseName}] ON PRIMARY (Name={DatabaseName}_data, FILENAME='{System.IO.Path.Combine(path, DatabaseName)}.mdf') LOG ON (Name={DatabaseName}_log, FILENAME='{System.IO.Path.Combine(path, DatabaseName)}_log.ldf')", connection);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public override void DropDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			using (connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB"))
			{
				connection.Open();
				command = new SqlCommand( $@"DROP DATABASE [{DatabaseName}]",connection);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

	}
}
