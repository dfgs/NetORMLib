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
	public class SqlInsertCommandBuilderUnitTest
	{

		
		[TestMethod]
		public void ShouldBuildExplicitInsert()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new Insert<Personn>(new Setter<Personn,string>( Personn.FirstName,"John"), new Setter<Personn, string>(Personn.LastName,"Doe"));
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("INSERT INTO [Personn] ([Personn].[FirstName], [Personn].[LastName]) VALUES (@FirstName0, @LastName1)", command.CommandText);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
		}



	

	}
}
