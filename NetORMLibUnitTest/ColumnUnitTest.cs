using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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



	}
}
