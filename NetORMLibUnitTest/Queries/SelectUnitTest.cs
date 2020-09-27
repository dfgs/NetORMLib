using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
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

			query = new Select(PersonnTable.LastName, PersonnTable.FirstName).From(TestDB.PersonnTable);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Columns.ElementAt(1));
			Assert.AreEqual(-1, query.Limit);
		}

		[TestMethod]
		public void ShouldOrderByColumnsASC()
		{
			ISelect query;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable).OrderBy(PersonnTable.LastName,PersonnTable.FirstName);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Orders.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Orders.ElementAt(1));
			Assert.AreEqual(OrderModes.ASC, query.OrderMode);
		}

		[TestMethod]
		public void ShouldOrderByColumnsDESC()
		{
			ISelect query;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable).OrderBy(OrderModes.DESC, PersonnTable.LastName, PersonnTable.FirstName);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Orders.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Orders.ElementAt(1));
			Assert.AreEqual(OrderModes.DESC, query.OrderMode);
		}

		[TestMethod]
		public void ShouldFilterByColumns()
		{
			ISelect query;

			query = new Select(PersonnTable.FirstName).From(TestDB.PersonnTable).Where(PersonnTable.FirstName.IsEqualTo("John"));
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(1, query.Filters.Count());
		}

		[TestMethod]
		public void ShouldSelectFirst()
		{
			ISelect query;

			query = new Select(PersonnTable.LastName, PersonnTable.FirstName).Top(10).From(TestDB.PersonnTable);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Columns.ElementAt(1));
			Assert.AreEqual(10, query.Limit);
		}

		
	}
}
