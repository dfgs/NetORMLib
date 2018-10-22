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

			query = new Select<Personn>(Personn.LastName, Personn.FirstName);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(Personn.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(Personn.FirstName, query.Columns.ElementAt(1));
		}

		[TestMethod]
		public void ShouldOrderByColumns()
		{
			ISelect query;

			query = new Select<Personn>(Personn.FirstName).OrderBy(Personn.LastName,Personn.FirstName);
			Assert.AreEqual(2, query.Orders.Count());
			Assert.AreEqual(Personn.LastName, query.Orders.ElementAt(0));
			Assert.AreEqual(Personn.FirstName, query.Orders.ElementAt(1));
		}


		[TestMethod]
		public void ShouldFilterByColumns()
		{
			ISelect query;

			query = new Select<Personn>(Personn.FirstName).Where(Personn.FirstName.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
