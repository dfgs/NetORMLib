using NetORMLib.ConnectionFactories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql.ConnectionFactories
{
	public class SqlLocalConnectionFactory : IConnectionFactory
	{
		private string path;

		public SqlLocalConnectionFactory(string Path)
		{
			this.path = Path;
		}
		public DbConnection CreateConnection()
		{
			return new System.Data.SqlClient.SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={path};Integrated Security=True;Connect Timeout=30");
		}
	}
}
