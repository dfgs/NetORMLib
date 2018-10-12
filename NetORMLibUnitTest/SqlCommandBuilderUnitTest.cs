using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlCommandBuilderUnitTest
	{

		[TestMethod]
		public void ShouldThrowExceptionIfNoTableSpecified()
		{
			IQuery query;
			ICommandBuilder builder;

			query = new Select(Personn.FirstNameColumn);
			builder = new SqlCommandBuilder();
			Assert.ThrowsException<InvalidOperationException>( () => builder.BuildCommand(query));
			
		}

		[TestMethod]
		public void ShouldBuildExplicitSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn).From<Personn>();
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName] FROM [Personn]", command.CommandText);
		}

		/// <summary>
		/// Command builder should include WHERE clause in SELECT query using "=" operator. 
		/// </summary>
		[TestMethod]
		public void ShouldBuildEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]=@FirstName0", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildSeveralEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsEqualTo("John"),Personn.LastNameColumn.IsEqualTo("Doe"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]=@FirstName0 AND [Personn].[LastName]=@LastName1", command.CommandText);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
		}

		[TestMethod]
		public void ShouldBuildNotEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsNotEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]<>@FirstName0", command.CommandText);
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

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsGreaterThan("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]>@FirstName0", command.CommandText);
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

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsLowerThan("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]<@FirstName0", command.CommandText);
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

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsGreaterOrEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]>=@FirstName0", command.CommandText);
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

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsLowerOrEqualTo("John"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName]<=@FirstName0", command.CommandText);
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

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsNull());
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName] IS NULL", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void ShouldBuildIsNotNullFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsNotNull());
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE [Personn].[FirstName] IS NOT NULL", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}


		[TestMethod]
		public void ShouldBuildSimpleAndFilterWith2Members()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(new AndFilter( Personn.FirstNameColumn.IsNotNull(),Personn.LastNameColumn.IsEqualTo("Doe") ) );
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE ([Personn].[FirstName] IS NOT NULL AND [Personn].[LastName]=@LastName0)", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@LastName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldBuildSimpleAndFilterWith3Members()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(new AndFilter(Personn.FirstNameColumn.IsNotNull(), Personn.LastNameColumn.IsEqualTo("Doe"), Personn.LastNameColumn.IsEqualTo("Doe")));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE ([Personn].[FirstName] IS NOT NULL AND [Personn].[LastName]=@LastName0 AND [Personn].[LastName]=@LastName1)", command.CommandText);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("@LastName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
		}

		[TestMethod]
		public void ShouldBuildNestedAndFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(new AndFilter(Personn.FirstNameColumn.IsEqualTo("John"),  new AndFilter(Personn.FirstNameColumn.IsNotNull(), Personn.LastNameColumn.IsEqualTo("Doe") ), Personn.LastNameColumn.IsEqualTo("Doe") ));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE ([Personn].[FirstName]=@FirstName0 AND ([Personn].[FirstName] IS NOT NULL AND [Personn].[LastName]=@LastName1) AND [Personn].[LastName]=@LastName2)", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@LastName2", command.Parameters[2].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[2].Value);
		}

		[TestMethod]
		public void ShouldBuildSimpleOrFilterWith2Members()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(Personn.FirstNameColumn, Personn.LastNameColumn).From<Personn>().Where(new OrFilter(Personn.FirstNameColumn.IsNotNull(), Personn.LastNameColumn.IsEqualTo("Doe")));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE ([Personn].[FirstName] IS NOT NULL OR [Personn].[LastName]=@LastName0)", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@LastName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[0].Value);
		}



	}
}
