﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateColumnUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateColumnUsingExplicitParameters()
		{
			ICreateColumn query;

			query = new CreateColumn(TestDB.PersonnTable, PersonnTable.PersonnID);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(PersonnTable.PersonnID, query.Column);
			Assert.ThrowsException<ArgumentNullException>(() => new CreateColumn(TestDB.PersonnTable, null));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateColumn(null, PersonnTable.FirstName));
		}



	}
}
