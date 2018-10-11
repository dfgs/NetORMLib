using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlCommandBuilderUnitTest
	{
		[TestMethod]
		public void ShouldBuildImplicitSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>();
			builder = new SqlCommandBuilder();
			command=builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn]", command.CommandText);
		}

		[TestMethod]
		public void ShouldBuildExplicitSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>(Personn.FirstNameColumn);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName] FROM [Personn]", command.CommandText);
		}

		[TestMethod]
		public void ShouldBuildEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]=@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildNotEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsNotEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]<>@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildGreaterFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsGreaterThan("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]>@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildLowerFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsLowerThan("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]<@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildGreaterOrEqualFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsGreaterOrEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]>=@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildLowerOrEqualFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsLowerOrEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName]<=@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildIsNullFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsNull());
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName] IS NULL", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void ShouldBuildIsNotNullFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select<Personn>().Where(Personn.FirstNameColumn.IsNotNull());
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [FirstName], [LastName] FROM [Personn] WHERE [FirstName] IS NOT NULL", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}


	}
}
