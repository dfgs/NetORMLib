using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class TableUnitTest
	{
		[TestMethod]
		public void ShouldReturnImplicitTableName()
		{
			Assert.AreEqual("Personn", Table<Personn>.Name);
		}
		[TestMethod]
		public void ShouldReturnExlicitTableName()
		{
			Assert.AreEqual("Personn", Table<PersonnExplicit>.Name);
		}
	}
}
