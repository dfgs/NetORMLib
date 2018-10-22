using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.CommandBuilders;
using NetORMLib.Databases;
using NetORMLib.Queries;
using NetORMLib.Sql.CommandBuilders;
using NetORMLib.Sql.Databases;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class DatabaseUnitTest
	{
		private DbConnection OnCreateConnection(string Folder)
		{
			return new System.Data.SqlClient.SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\{Folder}\UnitTestDatabase.mdf;Integrated Security=True;Connect Timeout=30");
		}

		/*
		 * [TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectRows")]
		public void ShouldSelectRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			rows=database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectRowsConsecutively")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectRowsConsecutively")]
		public void ShouldSelectRowsConsecutively()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectRowsConsecutively");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);
		}


		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldCreateTransactionWithoutCommands")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldCreateTransactionWithoutCommands")]
		public void ShouldCreateTransactionWithoutCommands()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;

			connection = OnCreateConnection("ShouldCreateTransactionWithoutCommands");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.EndTransaction(true);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldNotCreateTwoTransactions")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldNotCreateTwoTransactions")]
		public void ShouldNotCreateTwoTransactions()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;

			connection = OnCreateConnection("ShouldNotCreateTwoTransactions");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			Assert.ThrowsException<InvalidOperationException>(()=>database.BeginTransaction());
			database.EndTransaction(true);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldNotCommitTransactionIfDoesntExist")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldNotCommitTransactionIfDoesntExist")]
		public void ShouldNotCommitTransactionIfDoesntExist()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;

			connection = OnCreateConnection("ShouldNotCommitTransactionIfDoesntExist");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			Assert.ThrowsException<InvalidOperationException>(() => database.EndTransaction(false));
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldDeleteRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldDeleteRows")]
		public void ShouldDeleteRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldDeleteRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Ned")));

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(2, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldCommitDeleteRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldCommitDeleteRows")]
		public void ShouldCommitDeleteRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldCommitDeleteRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Ned")));
			database.EndTransaction(true);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(2, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
		}
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldRevertDeleteRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldRevertDeleteRows")]
		public void ShouldRevertDeleteRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldRevertDeleteRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Ned")));
			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Marje")));
			database.EndTransaction(false);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectWithinDeleteTransaction")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectWithinDeleteTransaction")]
		public void ShouldSelectWithinDeleteTransaction()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectWithinDeleteTransaction");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Ned")));
			database.Execute(new Delete().From<Personn>().Where(Personn.LastName.IsEqualTo("Marje")));

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(1, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);

			database.EndTransaction(false);

		}



		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldUpdateRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldUpdateRows")]
		public void ShouldUpdateRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldUpdateRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.Execute(new Update().Set(Personn.LastName,"Maud").Where(Personn.LastName.IsEqualTo("Ned")));

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);
		}

		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldCommitUpdateRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldCommitUpdateRows")]
		public void ShouldCommitUpdateRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldCommitUpdateRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Update().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));
			database.EndTransaction(true);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);
		}
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldRevertUpdateRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldRevertUpdateRows")]
		public void ShouldRevertUpdateRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldRevertUpdateRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Update().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));
			database.EndTransaction(false);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);
		}

		
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectWithinUpdateTransaction")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectWithinUpdateTransaction")]
		public void ShouldSelectWithinUpdateTransaction()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectWithinUpdateTransaction");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Update().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);

			database.EndTransaction(false);

		}



		/*
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldInsertRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldInsertRows")]
		public void ShouldInsertRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldInsertRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.Execute(new Insert<Personn>() );

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);
		}
		
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldCommitInsertRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldCommitInsertRows")]
		public void ShouldCommitInsertRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldCommitInsertRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Insert<Personn>().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));
			database.EndTransaction(true);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);
		}
		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldRevertInsertRows")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldRevertInsertRows")]
		public void ShouldRevertInsertRows()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldRevertInsertRows");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Insert<Personn>().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));
			database.EndTransaction(false);

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Ned", rows[2].LastName);
		}


		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectWithinInsertTransaction")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectWithinInsertTransaction")]
		public void ShouldSelectWithinInsertTransaction()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectWithinInsertTransaction");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.BeginTransaction();
			database.Execute(new Insert<Personn>().Set(Personn.LastName, "Maud").Where(Personn.LastName.IsEqualTo("Ned")));

			rows = database.Execute(new Select(Personn.FirstName, Personn.LastName).From<Personn>()).ToArray();
			Assert.AreEqual(3, rows.Length);
			Assert.AreEqual("Simpson", rows[0].FirstName);
			Assert.AreEqual("Homer", rows[0].LastName);
			Assert.AreEqual("Simpson", rows[1].FirstName);
			Assert.AreEqual("Marje", rows[1].LastName);
			Assert.AreEqual("Flanders", rows[2].FirstName);
			Assert.AreEqual("Maud", rows[2].LastName);

			database.EndTransaction(false);

		}
	






		[TestMethod]
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldGetTables")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldGetTables")]
		public void ShouldGetTables()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			string[] tables;

			connection = OnCreateConnection("ShouldGetTables");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			tables = database.GetTables().ToArray();

			Assert.AreEqual(1, tables.Length);
			Assert.AreEqual("Personn", tables[0]);
		}

		[TestMethod]
		public void ShouldCreateTableWithPrimaryKeyAndIdentity()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			IDatabaseCreator databaseCreator;
			Database database;

			Directory.Delete("ShouldCreateTableWithPrimaryKeyAndIdentity", true);
			Directory.CreateDirectory("ShouldCreateTableWithPrimaryKeyAndIdentity");
			databaseCreator = new SqlLocalDatabaseCreator("ShouldCreateTableWithPrimaryKeyAndIdentity", "ShouldCreateTableWithPrimaryKeyAndIdentity");
			connection = OnCreateConnection("ShouldCreateTableWithPrimaryKeyAndIdentity");
			commandBuilder = new SqlCommandBuilder();
			database = new Database(connection, commandBuilder);

			database.Execute(new CreateTable<Personn>(Personn.PersonnID, Personn.FirstName, Personn.LastName));

			databaseCreator.DropDatabase();
		}


		//*/


	}
}
