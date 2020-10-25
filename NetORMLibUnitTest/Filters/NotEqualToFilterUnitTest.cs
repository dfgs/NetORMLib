using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class NotEqualToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsNotEqualToFilter<string> filter;

			filter = JobTable.Description.IsNotEqualTo("test");
			Assert.AreEqual("C<>test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsNotEqualTo("test").Or(JobTable.Description.IsNotEqualTo("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsNotEqualTo("test").Or(JobTable.Description.IsNotEqualTo("test2").Or(JobTable.Description.IsNotEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNotEqualTo("test").Or(JobTable.Description.IsNotEqualTo("test2")).Or(JobTable.Description.IsNotEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsNotEqualTo("test").And(JobTable.Description.IsNotEqualTo("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsNotEqualTo("test").And(JobTable.Description.IsNotEqualTo("test2").And(JobTable.Description.IsNotEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNotEqualTo("test").And(JobTable.Description.IsNotEqualTo("test2")).And(JobTable.Description.IsNotEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
