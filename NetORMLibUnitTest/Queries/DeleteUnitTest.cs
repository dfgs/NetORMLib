using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class DeleteUnitTest
	{
		
		[TestMethod]
		public void ShouldDelete()
		{
			IDelete query;

			query = new Delete<PersonnTable>();
		}

		
		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IDelete query;

			query = new Delete<PersonnTable>().Where(PersonnTable.FirstName.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
