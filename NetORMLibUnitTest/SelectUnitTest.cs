using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SelectUnitTest
	{
		[TestMethod]
		public void ShouldSelectAllColumnsUsingDefaultParameters()
		{
			ISelect<Personn> query;

			query=new Select<Personn>();
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(Personn.FirstNameColumn, query.Columns.ElementAt(0));
			Assert.AreEqual(Personn.LastNameColumn, query.Columns.ElementAt(1));
		}
		[TestMethod]
		public void ShouldSelectColumnsUsingExplicitParameters()
		{
			ISelect<Personn> query;

			query = new Select<Personn>(Personn.LastNameColumn, Personn.FirstNameColumn);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(Personn.LastNameColumn, query.Columns.ElementAt(0));
			Assert.AreEqual(Personn.FirstNameColumn, query.Columns.ElementAt(1));
		}

		[TestMethod]
		public void ShouldOrderByColumns()
		{
			ISelect<Personn> query;

			query = new Select<Personn>(Personn.FirstNameColumn).OrderBy(Personn.LastNameColumn,Personn.FirstNameColumn);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(Personn.LastNameColumn, query.Orders.ElementAt(0));
			Assert.AreEqual(Personn.FirstNameColumn, query.Orders.ElementAt(1));
		}


		[TestMethod]
		public void ShouldFilterByColumns()
		{
			ISelect<Personn> query;

			query = new Select<Personn>(Personn.FirstNameColumn).Where(Personn.FirstNameColumn.Equals("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
