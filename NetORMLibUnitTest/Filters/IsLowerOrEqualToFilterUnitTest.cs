using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsLowerOrEqualToToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsLowerOrEqualToFilter<string> filter;

			filter = JobTable.Description.IsLowerOrEqualTo("test");
			Assert.AreEqual("C<=test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsLowerOrEqualTo("test").Or(JobTable.Description.IsLowerOrEqualTo("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsLowerOrEqualTo("test").Or(JobTable.Description.IsLowerOrEqualTo("test2").Or(JobTable.Description.IsLowerOrEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsLowerOrEqualTo("test").Or(JobTable.Description.IsLowerOrEqualTo("test2")).Or(JobTable.Description.IsLowerOrEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsLowerOrEqualTo("test").And(JobTable.Description.IsLowerOrEqualTo("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsLowerOrEqualTo("test").And(JobTable.Description.IsLowerOrEqualTo("test2").And(JobTable.Description.IsLowerOrEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsLowerOrEqualTo("test").And(JobTable.Description.IsLowerOrEqualTo("test2")).And(JobTable.Description.IsLowerOrEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
