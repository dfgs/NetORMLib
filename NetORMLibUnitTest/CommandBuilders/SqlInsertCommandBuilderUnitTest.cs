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
	public class SqlInsertCommandBuilderUnitTest
	{

		
		[TestMethod]
		public void ShouldBuildExplicitInsert()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Insert<PersonnTable>().Set(PersonnTable.FirstName, "John").Set(PersonnTable.LastName, "Doe").Set(PersonnTable.SecureCode,3);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("INSERT INTO [Personn] ([Personn].[FirstName], [Personn].[LastName], [Personn].[SecureCode]) VALUES (@FirstName0, @LastName1, @SecureCode2)", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@SecureCode2", command.Parameters[2].ParameterName);
			Assert.AreEqual(3, command.Parameters[2].Value);
		}
		[TestMethod]
		public void ShouldBuildExplicitInsertWithNullValue()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Insert<PersonnTable>().Set(PersonnTable.FirstName, "John").Set(PersonnTable.LastName, "Doe").Set(PersonnTable.SecureCode, null);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("INSERT INTO [Personn] ([Personn].[FirstName], [Personn].[LastName], [Personn].[SecureCode]) VALUES (@FirstName0, @LastName1, @SecureCode2)", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@SecureCode2", command.Parameters[2].ParameterName);
			Assert.AreEqual(null, command.Parameters[2].Value);
		}
		[TestMethod]
		public void ShouldBuildImplicitInsert()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Insert<PersonnTable>();
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("INSERT INTO [Personn]", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}



	}
}
