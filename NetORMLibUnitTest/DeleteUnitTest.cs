using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class DeleteUnitTest
	{
		
		[TestMethod]
		public void ShouldDelete()
		{
			IDelete query;

			query = new Delete().From<Personn>();
		}

		
		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IDelete query;

			query = new Delete().From<Personn>().Where(Personn.FirstName.IsEqualTo("John"));
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}
