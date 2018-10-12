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
		public void ShouldSelectColumnsUsingExplicitParameters()
		{
			ISelect query;

			query = new Select(Personn.LastNameColumn, Personn.FirstNameColumn).From<Personn>();
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(Personn.LastNameColumn, query.Columns.ElementAt(0));
			Assert.AreEqual(Personn.FirstNameColumn, query.Columns.ElementAt(1));
		}

		[TestMethod]
		public void ShouldOrderByColumns()
		{
			ISelect query;

			query = new Select(Personn.FirstNameColumn).From<Personn>().OrderBy(Personn.LastNameColumn,Personn.FirstNameColumn);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(Personn.LastNameColumn, query.Orders.ElementAt(0));
			Assert.AreEqual(Personn.FirstNameColumn, query.Orders.ElementAt(1));
		}


		[TestMethod]
		public void ShouldFilterByColumns()
		{
			ISelect query;

			query = new Select(Personn.FirstNameColumn).From<Personn>().Where(Personn.FirstNameColumn.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
