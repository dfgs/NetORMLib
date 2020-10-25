using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsLowerThanToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsLowerThanFilter<string> filter;

			filter = JobTable.Description.IsLowerThan("test");
			Assert.AreEqual("C<test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsLowerThan("test").Or(JobTable.Description.IsLowerThan("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsLowerThan("test").Or(JobTable.Description.IsLowerThan("test2").Or(JobTable.Description.IsLowerThan("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsLowerThan("test").Or(JobTable.Description.IsLowerThan("test2")).Or(JobTable.Description.IsLowerThan("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsLowerThan("test").And(JobTable.Description.IsLowerThan("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsLowerThan("test").And(JobTable.Description.IsLowerThan("test2").And(JobTable.Description.IsLowerThan("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsLowerThan("test").And(JobTable.Description.IsLowerThan("test2")).And(JobTable.Description.IsLowerThan("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
