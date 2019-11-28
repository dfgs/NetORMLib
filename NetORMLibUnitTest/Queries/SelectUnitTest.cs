using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class SelectUnitTest
	{
		
		[TestMethod]
		public void ShouldSelectColumnsUsingExplicitParameters()
		{
			ISelect query;

			query = new Select<PersonnTable>(PersonnTable.LastName, PersonnTable.FirstName);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Columns.ElementAt(1));
		}

		[TestMethod]
		public void ShouldOrderByColumns()
		{
			ISelect query;

			query = new Select<PersonnTable>(PersonnTable.FirstName).OrderBy(PersonnTable.LastName,PersonnTable.FirstName);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Orders.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Orders.ElementAt(1));
		}


		[TestMethod]
		public void ShouldFilterByColumns()
		{
			ISelect query;

			query = new Select<PersonnTable>(PersonnTable.FirstName).Where(PersonnTable.FirstName.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
