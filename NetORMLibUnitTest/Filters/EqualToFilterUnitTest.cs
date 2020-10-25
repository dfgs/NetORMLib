using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class EqualToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsEqualToFilter<string> filter;

			filter = JobTable.Description.IsEqualTo("test");
			Assert.AreEqual("C=test", filter.Format("C","test"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsEqualTo("test").Or(JobTable.Description.IsEqualTo("test2"));
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsEqualTo("test").Or(JobTable.Description.IsEqualTo("test2").Or(JobTable.Description.IsEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsEqualTo("test").Or(JobTable.Description.IsEqualTo("test2")).Or(JobTable.Description.IsEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsEqualTo("test").And(JobTable.Description.IsEqualTo("test2"));
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsEqualTo("test").And(JobTable.Description.IsEqualTo("test2").And(JobTable.Description.IsEqualTo("test3")));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsEqualTo("test").And(JobTable.Description.IsEqualTo("test2")).And(JobTable.Description.IsEqualTo("test3"));
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
