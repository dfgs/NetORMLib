using NetORMLib.ConnectionFactories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql.ConnectionFactories
{
	public class SqlConnectionFactory : IConnectionFactory
	{
		private string server;
		private string databaseName;

		public SqlConnectionFactory(string Server,string DatabaseName)
		{
			this.server = Server;
			this.databaseName = DatabaseName;
		}
		public DbConnection CreateConnection()
		{
			return new System.Data.SqlClient.SqlConnection($@"Server={server};Database={databaseName};Trusted_Connection=True;Connect Timeout=30");
		}
	}
}
