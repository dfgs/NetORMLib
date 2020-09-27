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
	public class JoinedTableUnitTest
	{
		
		
		[TestMethod]
		public void ShouldCreateJoinedTables()
		{
			IJoinedTables joinedTables;

			joinedTables = TestDB.PersonnTable.Join(TestDB.JobTypeTable.On(PersonnTable.FirstName, JobTypeTable.Name)).Join(TestDB.JobTable.On(PersonnTable.FirstName,JobTable.Description));
			Assert.IsNotNull(joinedTables);
			Assert.AreEqual(2,joinedTables.Joins.Count());
			Assert.AreEqual(TestDB.PersonnTable, joinedTables.FirstTable);
		}

		

	}
}
