using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.Queries;
using NetORMLib.Tables;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Tables
{
	[TestClass]
	public class TableUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateJoin()
		{
			IJoin join;

			join = TestDB.JobTypeTable.On( PersonnTable.FirstName, JobTypeTable.Name);
			Assert.IsNotNull(join);
			Assert.AreEqual(TestDB.JobTypeTable, join.OtherTable);
			Assert.AreEqual(PersonnTable.FirstName, join.FirstColumn);
			Assert.AreEqual(JobTypeTable.Name, join.SecondColumn);
		}
		[TestMethod]
		public void ShouldCreateJoinedTables()
		{
			IJoinedTables joinedTables;

			joinedTables = TestDB.PersonnTable.Join(TestDB.JobTypeTable.On(PersonnTable.FirstName, JobTypeTable.Name));
			Assert.IsNotNull(joinedTables);
			Assert.AreEqual(1,joinedTables.Joins.Count());
			Assert.AreEqual(TestDB.PersonnTable, joinedTables.FirstTable);
		}

		

	}
}
