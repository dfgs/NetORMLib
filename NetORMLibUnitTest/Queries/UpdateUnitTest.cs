using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class UpdateUnitTest
	{
		
		[TestMethod]
		public void ShouldUpdate()
		{
			IUpdate query;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName,"John").Set(PersonnTable.SecureCode,3);
			Assert.AreEqual(2, query.Setters.Count());
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
		}

		
		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IUpdate query;

			query = new Update(TestDB.PersonnTable).Set(PersonnTable.FirstName, "John").Where(PersonnTable.FirstName.IsEqualTo("Homer"));
			Assert.AreEqual(1, query.Setters.Count());
			Assert.AreEqual(1, query.Filters.Count());
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
		}


	}
}
