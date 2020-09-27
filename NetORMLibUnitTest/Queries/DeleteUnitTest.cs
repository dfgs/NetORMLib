using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class DeleteUnitTest
	{
		
		[TestMethod]
		public void ShouldDelete()
		{
			IDelete query;

			query = new Delete().From(TestDB.PersonnTable);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
		}


		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IDelete query;

			query = new Delete().From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsEqualTo("John"));
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
