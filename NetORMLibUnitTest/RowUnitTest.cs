using System;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class RowUnitTest
	{
		private void AssertFailsDynamic(Action Action)
		{
			try
			{
				Action();
				Assert.Fail();
			}
			catch (Exception)
			{
			}
			
		}
		[TestMethod]
		public void ShouldGetDynamicProperties()
		{
			dynamic row;

			row = new Row(Table<Personn>.Columns);
			Assert.AreEqual("Homer", row.FirstName);
			Assert.AreEqual("Simpson", row.LastName);
		}
		[TestMethod]
		public void ShouldNotGetInvalidProperties()
		{
			dynamic row;

			row = new Row(Table<Personn>.Columns);
			Assert.AreEqual("Homer", row.FirstName);
			Assert.AreEqual("Simpson", row.LastName);
			AssertFailsDynamic(() => { int age = row.Age; });
		}

		[TestMethod]
		public void ShouldSetDynamicProperties()
		{
			dynamic row;

			row = new Row(Table<Personn>.Columns);
			row.FirstName = "John";
			row.LastName = "Doe";
			Assert.AreEqual("John", row.FirstName);
			Assert.AreEqual("Doe", row.LastName);
		}

		[TestMethod]
		public void ShouldNotSetInvalidProperties()
		{
			dynamic row;

			row = new Row(Table<Personn>.Columns);
			AssertFailsDynamic(() => { row.Age = 2; });
			AssertFailsDynamic(() => { row.FirstName = 2; });

		}

	}
}
