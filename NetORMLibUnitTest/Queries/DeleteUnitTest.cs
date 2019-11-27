using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class DeleteUnitTest
	{
		
		[TestMethod]
		public void ShouldDelete()
		{
			IDelete query;

			query = new Delete<Personn>();
		}

		
		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IDelete query;

			query = new Delete<Personn>().Where(Personn.FirstName.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
