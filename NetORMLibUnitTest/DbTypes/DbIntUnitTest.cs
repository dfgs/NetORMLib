using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.DbTypes;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.DbTypes
{
	[TestClass]
	public class DbIntUnitTest
	{
		
		
		[TestMethod]
		public void ShouldEquals()
		{
			DbInt a, b;

			a = new DbInt(1);
			b = new DbInt(1);
			Assert.IsTrue(a.Equals(1));
			Assert.IsTrue(b.Equals(1));
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEquals()
		{
			DbInt a, b;

			a = new DbInt(1);
			b = new DbInt(2);
			Assert.IsFalse(a.Equals(2));
			Assert.IsFalse(b.Equals(1));
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}
		[TestMethod]
		public void ShouldGetCLRValue()
		{
			DbInt a;

			a = new DbInt(1);
			
			Assert.AreEqual(1, a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertFromCLRValue()
		{
			DbInt a;

			a = 1;
			Assert.AreEqual(1, a.GetCLRValue());
		}

		[TestMethod]
		public void ShouldImplicitConvertToCLRValue()
		{
			DbInt a;
			int b;

			a = new DbInt(1);
			b = a;
			Assert.AreEqual(a.GetCLRValue(), b);
		}
	}
}
