using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class ColumnUnitTest
	{
		
		[TestMethod]
		public void ShouldDetectIfColumnIsNullable()
		{
			Assert.AreEqual(false, PersonnTable.FirstName.IsNullable);
			Assert.AreEqual(false, PersonnTable.PersonnID.IsNullable);
			Assert.AreEqual(true, PersonnTable.SecureCode.IsNullable);
			Assert.AreEqual(true, PersonnTable.Job.IsNullable);
		}

		



	}
}
