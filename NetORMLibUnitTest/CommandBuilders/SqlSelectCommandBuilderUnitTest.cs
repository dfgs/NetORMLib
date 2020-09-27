using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.CommandBuilders
{
	[TestClass]
	public class SqlSelectCommandBuilderUnitTest
	{

		

		[TestMethod]
		public void ShouldBuildExplicitSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName] FROM [Personn]", command.CommandText);
		}

		[TestMethod]
		public void ShouldBuildExplicitSelectWithJoinedTable()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(PersonnTable.FirstName,ChildTable.FirstName).From(TestDB.PersonnTable.Join(TestDB.ChildTable.On(PersonnTable.PersonnID,ChildTable.PersonnID)));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Child].[FirstName] FROM [Personn] INNER JOIN [Child] ON [Personn].[PersonnID] = [Child].[PersonnID]", command.CommandText);

			query = new Select(PersonnTable.FirstName, ChildTable.FirstName).From(
				TestDB.PersonnTable.Join(TestDB.ChildTable.On(PersonnTable.PersonnID, ChildTable.PersonnID))
				.Join(TestDB.JobTypeTable.On(ChildTable.FirstName,JobTypeTable.Description))
				);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Child].[FirstName] FROM [Personn] INNER JOIN [Child] ON [Personn].[PersonnID] = [Child].[PersonnID] INNER JOIN [JobType] ON [Child].[FirstName] = [JobType].[Description]", command.CommandText);
		}


		[TestMethod]
		public void ShouldBuildExplicitSelectFirst()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(PersonnTable.FirstName).Top(10).From(TestDB.PersonnTable);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT TOP(10) [Personn].[FirstName] FROM [Personn]", command.CommandText);
		}

		[TestMethod]
		public void ShouldBuildOrderedASCSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable).OrderBy(PersonnTable.FirstName);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName] FROM [Personn] ORDER BY [Personn].[FirstName] ASC", command.CommandText);
		}

		[TestMethod]
		public void ShouldBuildOrderedDESCSelect()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable).OrderBy(NetORMLib.OrderModes.DESC, PersonnTable.FirstName);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName] FROM [Personn] ORDER BY [Personn].[FirstName] DESC", command.CommandText);
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsEqualTo("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsEqualTo("John"), PersonnTable.LastName.IsEqualTo("Doe"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsNotEqualTo("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsGreaterThan("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsLowerThan("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsGreaterOrEqualTo("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsLowerOrEqualTo("John"));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsNull());
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsNotNull());
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(new AndFilter(PersonnTable.FirstName.IsNotNull(), PersonnTable.LastName.IsEqualTo("Doe") ) );
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(new AndFilter(PersonnTable.FirstName.IsNotNull(), PersonnTable.LastName.IsEqualTo("Doe"), PersonnTable.LastName.IsEqualTo("Doe")));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(new AndFilter(PersonnTable.FirstName.IsEqualTo("John"),  new AndFilter(PersonnTable.FirstName.IsNotNull(), PersonnTable.LastName.IsEqualTo("Doe") ), PersonnTable.LastName.IsEqualTo("Doe") ));
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

			query = new Select(PersonnTable.FirstName, PersonnTable.LastName).From(TestDB.PersonnTable).Where(new OrFilter(PersonnTable.FirstName.IsNotNull(), PersonnTable.LastName.IsEqualTo("Doe")));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT [Personn].[FirstName], [Personn].[LastName] FROM [Personn] WHERE ([Personn].[FirstName] IS NOT NULL OR [Personn].[LastName]=@LastName0)", command.CommandText);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("@LastName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[0].Value);
		}


		

	}
}
