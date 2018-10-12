using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.CommandBuilders;
using NetORMLib.Databases;
using NetORMLib.Queries;
using NetORMLib.Sql;
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

		[TestMethod]
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
		[DeploymentItem(@"UnitTestDatabase.mdf", "ShouldSelectWithinATransaction")]
		[DeploymentItem(@"UnitTestDatabase_log.ldf", "ShouldSelectWithinATransaction")]
		public void ShouldSelectWithinATransaction()
		{
			DbConnection connection;
			ICommandBuilder commandBuilder;
			Database database;
			dynamic rows;

			connection = OnCreateConnection("ShouldSelectWithinATransaction");
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
		//*/


	}
}
