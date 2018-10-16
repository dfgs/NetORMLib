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
	public class SqlUpdateCommandBuilderUnitTest
	{

		[TestMethod]
		public void ShouldThrowExceptionIfNoTableSpecified()
		{
			IQuery query;
			ICommandBuilder builder;

			query = new Update();
			builder = new SqlCommandBuilder();
			Assert.ThrowsException<InvalidOperationException>( () => builder.BuildCommand(query));
			
		}

		[TestMethod]
		public void ShouldBuildExplicitUpdate()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update().Set(Personn.FirstName,"John").Set(Personn.LastName,"Doe");
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("UPDATE [Personn] SET [Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1", command.CommandText);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
		}



		[TestMethod]
		public void ShouldBuildEqualsFilter()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Update().Set(Personn.FirstName, "John").Set(Personn.LastName, "Doe").Where(Personn.FirstName.IsEqualTo("Homer"));
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

			query = new Update().Set(Personn.FirstName, "John").Set(Personn.LastName, "Doe").Where(Personn.FirstName.IsEqualTo("Homer"),Personn.LastName.IsEqualTo("Simpson"));
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
