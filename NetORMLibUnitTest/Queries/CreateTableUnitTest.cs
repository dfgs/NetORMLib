using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateTableUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateTableColumnsUsingExplicitParameters()
		{
			ICreateTable query;

			query = new CreateTable(TestDB.PersonnTable, PersonnTable.LastName, PersonnTable.FirstName);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(PersonnTable.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, query.Columns.ElementAt(1));
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
		}



	}
}
