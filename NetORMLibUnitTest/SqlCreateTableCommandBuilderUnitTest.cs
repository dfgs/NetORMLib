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
	public class SqlCreateTableCommandBuilderUnitTest
	{

		[TestMethod]
		public void ShouldThrowExceptionIfNoColumnSpecified()
		{
			ICommandBuilder builder;

			builder = new SqlCommandBuilder();
			Assert.ThrowsException<ArgumentNullException>( () => new CreateTable<Personn>());
			
		}

		[TestMethod]
		public void ShouldBuildCreateTableWithPrimaryKeyAndIdentity()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateTable<Personn>(Personn.PersonnID, Personn.FirstName, Personn.LastName, Personn.Job);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("CREATE TABLE [Personn] ([Personn].[PersonnID] int IDENTITY(1, 1) NOT NULL, [Personn].[FirstName] nvarchar(MAX) NOT NULL, [Personn].[LastName] nvarchar(MAX) NOT NULL, [Personn].[Job] nvarchar(MAX) NULL) CONSTRAINT [PK_Personn] PRIMARY KEY CLUSTERED ([PersonnID] ASC)", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}
		[TestMethod]
		public void ShouldBuildCreateTableWithoutPrimaryKey()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateTable<Job>(Job.Description,Job.Company);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("CREATE TABLE [Job] ([Job].[Description] nvarchar(MAX) NOT NULL, [Job].[Company] nvarchar(MAX) NULL)", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void ShouldBuildCreateTableWithoutIdentity()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateTable<JobType>(JobType.JobTypeID,JobType.Description);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("CREATE TABLE [JobType] ([JobType].[JobTypeID] int NOT NULL, [JobType].[Description] nvarchar(MAX) NOT NULL) CONSTRAINT [PK_JobType] PRIMARY KEY CLUSTERED ([JobTypeID] ASC)", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}





	}
}
