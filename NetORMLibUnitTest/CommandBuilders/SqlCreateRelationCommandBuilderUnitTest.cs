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
	public class SqlCreateRelationCommandBuilderUnitTest
	{

		

		[TestMethod]
		public void ShouldBuildCreateRelation()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;


			query = new CreateRelation<PersonnTable, JobTypeTable, int>(TestDB.JobTypeTable, PersonnTable.PersonnID, JobTypeTable.JobTypeID); ;
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("ALTER TABLE [JobType] WITH CHECK ADD CONSTRAINT [FK_JobType_JobTypeID_Personn] FOREIGN KEY ([JobTypeID]) REFERENCES [Personn] ([PersonnID])", command.CommandText);
			Assert.AreEqual(0, command.Parameters.Count);
		}




	}
}
