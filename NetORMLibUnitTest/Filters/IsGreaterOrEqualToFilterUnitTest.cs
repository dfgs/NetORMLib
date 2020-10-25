using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsGreaterOrEqualToToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsGreaterOrEqualToFilter<string> filter;

			filter = JobTable.Description.IsGreaterOrEqualTo("test");
			Assert.AreEqual("C>=test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsGreaterOrEqualTo("test").Or(JobTable.Description.IsGreaterOrEqualTo("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsGreaterOrEqualTo("test").Or(JobTable.Description.IsGreaterOrEqualTo("test2").Or(JobTable.Description.IsGreaterOrEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsGreaterOrEqualTo("test").Or(JobTable.Description.IsGreaterOrEqualTo("test2")).Or(JobTable.Description.IsGreaterOrEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsGreaterOrEqualTo("test").And(JobTable.Description.IsGreaterOrEqualTo("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsGreaterOrEqualTo("test").And(JobTable.Description.IsGreaterOrEqualTo("test2").And(JobTable.Description.IsGreaterOrEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsGreaterOrEqualTo("test").And(JobTable.Description.IsGreaterOrEqualTo("test2")).And(JobTable.Description.IsGreaterOrEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
