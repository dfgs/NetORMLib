using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlCreateColumnCommandBuilderUnitTest
	{


		[TestMethod]
		public void ShouldBuildCreateColumnWithPrimaryKeyAndIdentity()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateColumn<Personn>(Personn.PersonnID);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [Personn] ADD [PersonnID] int IDENTITY(1, 1) NOT NULL PRIMARY KEY", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}
		[TestMethod]
		public void ShouldBuildCreateColumnWithoutPrimaryKey()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateColumn<Job>(Job.Description);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [Job] ADD [Description] nvarchar(MAX) NOT NULL", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void ShouldBuildCreateColumnWithoutIdentity()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateColumn<JobType>(JobType.JobTypeID);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [JobType] ADD [JobTypeID] int NOT NULL PRIMARY KEY", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		


	}
}
