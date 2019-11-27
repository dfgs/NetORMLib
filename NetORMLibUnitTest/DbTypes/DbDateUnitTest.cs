using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.DbTypes;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.DbTypes
{
	[TestClass]
	public class DbDateUnitTest
	{
		
		
		[TestMethod]
		public void ShouldEquals()
		{
			DbDate a, b;

			a = new DbDate(new DateTime(1));
			b = new DbDate(new DateTime(1));
			Assert.IsTrue(a.Equals(new DateTime(1)));
			Assert.IsTrue(b.Equals(new DateTime(1)));
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEquals()
		{
			DbDate a, b;

			a = new DbDate(new DateTime(1));
			b = new DbDate(new DateTime(2));
			Assert.IsFalse(a.Equals(new DateTime(2)));
			Assert.IsFalse(b.Equals(new DateTime(1)));
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}
		[TestMethod]
		public void ShouldGetCLRValue()
		{
			DbDate a;

			a = new DbDate(new DateTime(1));
			
			Assert.AreEqual(new DateTime(1), a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertFromCLRValue()
		{
			DbDate a;

			a = new DateTime(1);
			Assert.AreEqual(new DateTime(1), a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertToCLRValue()
		{
			DbDate a;
			DateTime b;

			a = new DbDate(new DateTime(1));
			b = a;
			Assert.AreEqual(a.GetCLRValue(), b);
		}
	}
}
