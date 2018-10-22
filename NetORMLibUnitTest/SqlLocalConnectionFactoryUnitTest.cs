using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.ConnectionFactories;
using NetORMLib.Sql.ConnectionFactories;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlLocalConnectionFactoryUnitTest
	{
		[TestMethod]
		public void ShouldCreateConnection()
		{
			IConnectionFactory connectionFactory;
			DbConnection connection;

			connectionFactory = new SqlLocalConnectionFactory(@"c:\temp\test.mdf");
			connection = connectionFactory.CreateConnection();
			Assert.AreEqual($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\temp\test.mdf;Integrated Security=True;Connect Timeout=30", connection.ConnectionString);
		}
	}
}
