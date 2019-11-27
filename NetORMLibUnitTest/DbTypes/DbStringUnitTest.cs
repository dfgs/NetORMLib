using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.DbTypes;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.DbTypes
{
	[TestClass]
	public class DbStringUnitTest
	{
		
		[TestMethod]
		public void ShouldNotAllowNullValue()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new DbString(null));
		}
		[TestMethod]
		public void ShouldEquals()
		{
			DbString a, b;

			a = new DbString("a");
			b = new DbString("a");
			Assert.IsTrue(a.Equals("a"));
			Assert.IsTrue(b.Equals("a"));
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEquals()
		{
			DbString a, b;

			a = new DbString("a");
			b = new DbString("b");
			Assert.IsFalse(a.Equals("b"));
			Assert.IsFalse(b.Equals("a"));
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}
		[TestMethod]
		public void ShouldGetCLRValue()
		{
			DbString a;

			a = new DbString("a");
			
			Assert.AreEqual("a", a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertFromCLRValue()
		{
			DbString a;

			a = "a";
			Assert.AreEqual("a", a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertToCLRValue()
		{
			DbString a;
			string b;

			a = new DbString("a");
			b = a;
			Assert.AreEqual(a.GetCLRValue(), b);
		}
	}
}
