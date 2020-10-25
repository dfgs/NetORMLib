using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsNullToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsNullFilter filter;

			filter = JobTable.Description.IsNull();
			Assert.AreEqual("C IS NULL", filter.Format("C"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsNull().Or(JobTable.Description.IsNull());
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsNull().Or(JobTable.Description.IsNull().Or(JobTable.Description.IsNull()));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNull().Or(JobTable.Description.IsNull()).Or(JobTable.Description.IsNull());
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsNull().And(JobTable.Description.IsNull());
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsNull().And(JobTable.Description.IsNull().And(JobTable.Description.IsNull()));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNull().And(JobTable.Description.IsNull()).And(JobTable.Description.IsNull());
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
