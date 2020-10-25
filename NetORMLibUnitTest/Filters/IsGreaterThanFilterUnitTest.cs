using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsGreaterThanToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsGreaterThanFilter<string> filter;

			filter = JobTable.Description.IsGreaterThan("test");
			Assert.AreEqual("C>test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsGreaterThan("test").Or(JobTable.Description.IsGreaterThan("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsGreaterThan("test").Or(JobTable.Description.IsGreaterThan("test2").Or(JobTable.Description.IsGreaterThan("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsGreaterThan("test").Or(JobTable.Description.IsGreaterThan("test2")).Or(JobTable.Description.IsGreaterThan("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsGreaterThan("test").And(JobTable.Description.IsGreaterThan("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsGreaterThan("test").And(JobTable.Description.IsGreaterThan("test2").And(JobTable.Description.IsGreaterThan("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsGreaterThan("test").And(JobTable.Description.IsGreaterThan("test2")).And(JobTable.Description.IsGreaterThan("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
