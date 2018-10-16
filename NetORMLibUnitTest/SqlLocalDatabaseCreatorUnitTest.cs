using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Databases;
using NetORMLib.Sql.Databases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlLocalDatabaseCreatorUnitTest
	{

		/*private string OnCreateConnectionString()
		{
			return $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30";
		}*/
		/*private string OnCreateConnectionString(string Folder)
		{
			return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\{Folder}\UnitTestDatabase.mdf;Integrated Security=True;Connect Timeout=30";
		}
		//*/

		[TestMethod]
		public void ShouldDetectIfDatabaseDoesntExists()
		{
			IDatabaseCreator creator;

			creator = new SqlLocalDatabaseCreator("UnitTestDatabase", $@"{Directory.GetCurrentDirectory()}\ShouldDetectIfDatabaseDoesntExists");
			Assert.AreEqual(false, creator.DatabaseExists());
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldDetectIfDatabaseExists")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldDetectIfDatabaseExists")]
		public void ShouldDetectIfDatabaseExists()
		{
			IDatabaseCreator creator;

			creator = new SqlLocalDatabaseCreator("UnitTestDatabase", $@"{Directory.GetCurrentDirectory()}\ShouldDetectIfDatabaseExists");

			Assert.AreEqual(true, creator.DatabaseExists());
		}


		[TestMethod]
		public void ShouldCreateDatabase()
		{
			IDatabaseCreator creator;

			creator = new SqlLocalDatabaseCreator("UnitTestDatabase", $@"{Directory.GetCurrentDirectory()}\ShouldCreateDatabase");

			// When running several tests, sql local instance keeps running and keeps trace of previously created database
			try
			{
				creator.DropDatabase();
			}
			catch
			{
				Directory.Delete($@"{Directory.GetCurrentDirectory()}\ShouldCreateDatabase", true);
				Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\ShouldCreateDatabase");
			}

			Assert.AreEqual(false, creator.DatabaseExists());
			creator.CreateDatabase();
			Thread.Sleep(5000); // database creation seems to be async on engine side
			Assert.AreEqual(true, creator.DatabaseExists());
		
		}




	}
}
