using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.CommandBuilders;

using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.CommandBuilders
{
	[TestClass]
	public class SqlCreateConstraintCommandBuilderUnitTest
	{

		

		[TestMethod]
		public void ShouldBuildCreatePrimaryKeyConstraint()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;


			query = new CreateConstraint(TestDB.PersonnTable, ColumnConstraints.PrimaryKey, PersonnTable.PersonnID);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [Personn] ADD CONSTRAINT [AK_Personn_PersonnID] PRIMARY KEY ([PersonnID])", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void ShouldBuildCreateUniqueConstraint()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;


			query = new CreateConstraint(TestDB.PersonnTable, ColumnConstraints.Unique, PersonnTable.PersonnID,PersonnTable.FirstName);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [Personn] ADD CONSTRAINT [AK_Personn_PersonnID_FirstName] UNIQUE ([PersonnID], [FirstName])", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}


	}
}
