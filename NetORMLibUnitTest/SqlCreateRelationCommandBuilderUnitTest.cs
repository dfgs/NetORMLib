using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.DbTypes;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlCreateRelationCommandBuilderUnitTest
	{

		

		[TestMethod]
		public void ShouldBuildCreateRelation()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;
			

			query = new CreateRelation<Personn,JobType,DbInt>(Personn.PersonnID, JobType.JobTypeID);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [JobType] WITH CHECK ADD CONSTRAINT [FK_JobType_JobTypeID_Personn] FOREIGN KEY ([JobTypeID]) REFERENCES [Personn] ([PersonnID])", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}
		




	}
}
