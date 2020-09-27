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
	public class SqlUpdateCommandBuilderUnitTest
	{

		[TestMethod]
		public void ShouldThrowExceptionIfNoSetterSpecified()
		{
			IQuery query;
			ICommandBuilder builder;

			query = new Update(TestDB.PersonnTable);
			builder = new SqlCommandBuilder();
			Assert.ThrowsException<InvalidOperationException>( () => builder.BuildCommand(query));
			
		}

		[TestMethod]
		public void ShouldBuildExplicitUpdate()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName,"John").Set(PersonnTable.LastName,"Doe").Set(PersonnTable.SecureCode,3);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("UPDATE [Personn] SET [Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1, [Personn].[SecureCode]=@SecureCode2", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@SecureCode2", command.Parameters[2].ParameterName);
			Assert.AreEqual(3, command.Parameters[2].Value);
		}

		[TestMethod]
		public void ShouldBuildExplicitUpdateWithNullValue()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName, "John").Set(PersonnTable.LastName, "Doe").Set(PersonnTable.SecureCode, null);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("UPDATE [Personn] SET [Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1, [Personn].[SecureCode]=@SecureCode2", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@SecureCode2", command.Parameters[2].ParameterName);
			Assert.AreEqual(DBNull.Value, command.Parameters[2].Value);
		}

		[TestMethod]
		public void ShouldBuildEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName, "John").Set(PersonnTable.LastName, "Doe").Where(PersonnTable.FirstName.IsEqualTo("Homer"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("UPDATE [Personn] SET [Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1 WHERE [Personn].[FirstName]=@FirstName2", command.CommandText);
			Assert.AreEqual(3, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@FirstName2", command.Parameters[2].ParameterName);
			Assert.AreEqual("Homer", command.Parameters[2].Value);
		}
		
		[TestMethod]
		public void ShouldBuildSeveralEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName, "John").Set(PersonnTable.LastName, "Doe").Where(PersonnTable.FirstName.IsEqualTo("Homer"),PersonnTable.LastName.IsEqualTo("Simpson"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("UPDATE [Personn] SET [Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1 WHERE [Personn].[FirstName]=@FirstName2 AND [Personn].[LastName]=@LastName3", command.CommandText);
			Assert.AreEqual(4, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
			Assert.AreEqual("@FirstName2", command.Parameters[2].ParameterName);
			Assert.AreEqual("Homer", command.Parameters[2].Value);
			Assert.AreEqual("@LastName3", command.Parameters[3].ParameterName);
			Assert.AreEqual("Simpson", command.Parameters[3].Value);
		}

	

	}
}
