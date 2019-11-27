using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.CommandBuilders
{
	[TestClass]
	public class SqlSelectIdentityCommandBuilderUnitTest
	{

		

		[TestMethod]
		public void ShouldBuildExplicitSelectIdentity()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new SelectIdentity<Personn>();
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("SELECT @@IDENTITY", command.CommandText);
		}



	}
}
