using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class RowUnitTest
	{
		[TestMethod]
		public void ShouldGetAndSetValue()
		{
			Row<Personn> row;

			row = new Row<Personn>();
			Assert.AreEqual(row.GetValue(Personn.FirstName),null);
			row.SetValue(Personn.FirstName, "Homer");
			Assert.IsTrue(row.GetValue(Personn.FirstName).Equals("Homer"));
		}
		/*[TestMethod]
		public void ShouldGetAndSetDynamicValue()
		{
			dynamic  row;

			row = new Row<Personn>();
			Assert.AreEqual(row.FirstName, null);
			row.FirstName="Homer";
			Assert.IsTrue(row.FirstName.Equals("Homer"));
		}*/
	}
}
