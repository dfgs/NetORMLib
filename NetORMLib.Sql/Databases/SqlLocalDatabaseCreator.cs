﻿using NetORMLib.Databases;
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
		private readonly string path;

		public SqlLocalDatabaseCreator(string DatabaseName,string Path):base(DatabaseName)
		{
			this.path = Path;
		}

		private string CreateConnectionString()
		{
			return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.Combine(path, DatabaseName)}.mdf;Integrated Security=True;Connect Timeout=30";
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Ne pas supprimer d'objets plusieurs fois")]
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

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Ne pas supprimer d'objets plusieurs fois")]
		public override void CreateDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			using (connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB"))
			{
				connection.Open();
				command = new SqlCommand($"CREATE DATABASE [{DatabaseName}] ON PRIMARY (Name={DatabaseName}_data, FILENAME='{System.IO.Path.Combine(path, DatabaseName)}.mdf') LOG ON (Name={DatabaseName}_log, FILENAME='{System.IO.Path.Combine(path, DatabaseName)}_log.ldf')", connection);
				command.ExecuteNonQuery();
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Vérifier si les requêtes SQL présentent des failles de sécurité")]
		public override void DropDatabase()
		{
			SqlConnection connection;
			SqlCommand command;

			using (connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB"))
			{
				connection.Open();
				command = new SqlCommand( $@"DROP DATABASE [{DatabaseName}]",connection);
				command.ExecuteNonQuery();
			}
		}

	}
}
