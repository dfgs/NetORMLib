﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class InsertUnitTest
	{
		[TestMethod]
		public void ShouldInsert()
		{
			IInsert query;

			query = new Insert().Into(TestDB.PersonnTable);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(0, query.Setters.Count());
		}

		[TestMethod]
		public void ShouldInsertColumnsUsingExplicitParameters()
		{
			IInsert query;

			query = new Insert( ).Into(TestDB.PersonnTable).Set(PersonnTable.FirstName,"John").Set(PersonnTable.LastName,"Doe").Set(PersonnTable.SecureCode,3);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(3, query.Setters.Count());
			Assert.AreEqual(PersonnTable.FirstName, query.Setters.ElementAt(0).Column);
			Assert.AreEqual(PersonnTable.LastName, query.Setters.ElementAt(1).Column);
			Assert.AreEqual(PersonnTable.SecureCode, query.Setters.ElementAt(2).Column);
		}




	}
}
