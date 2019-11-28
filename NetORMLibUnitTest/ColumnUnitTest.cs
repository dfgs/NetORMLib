using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class ColumnUnitTest
	{
		
		[TestMethod]
		public void ShouldDetectIfColumnIsNullable()
		{
			Assert.AreEqual(false, Personn.FirstName.IsNullable);
			Assert.AreEqual(false, Personn.PersonnID.IsNullable);
			Assert.AreEqual(true, Personn.SecureCode.IsNullable);
			Assert.AreEqual(true, Personn.Job.IsNullable);
		}

		[TestMethod]
		public void ShouldGetAndSetValue()
		{
			Row<Personn> row;

			row = new Row<Personn>();
			Assert.AreEqual(null, Personn.FirstName.GetValue(row));
			Personn.FirstName.SetValue(row, "Homer");
			Assert.IsTrue(Personn.FirstName.GetValue(row).Equals("Homer"));
		}

		[TestMethod]
		public void ShouldGetDefaultValue()
		{
			Row<Personn> row;

			row = new Row<Personn>();
			Assert.IsTrue(Personn.Job.GetValue(row).Equals("No job"));


		}
	}
}
