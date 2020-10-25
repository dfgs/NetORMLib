using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class IsNotNullToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IIsNotNullFilter filter;

			filter = JobTable.Description.IsNotNull();
			Assert.AreEqual("C IS NOT NULL", filter.Format("C"));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter filter;

			filter = JobTable.Description.IsNotNull().Or(JobTable.Description.IsNotNull());
			Assert.AreEqual(2,filter.Members.Count());

			filter = JobTable.Description.IsNotNull().Or(JobTable.Description.IsNotNull().Or(JobTable.Description.IsNotNull()));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNotNull().Or(JobTable.Description.IsNotNull()).Or(JobTable.Description.IsNotNull());
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter filter;

			filter = JobTable.Description.IsNotNull().And(JobTable.Description.IsNotNull());
			Assert.AreEqual(2, filter.Members.Count());

			filter = JobTable.Description.IsNotNull().And(JobTable.Description.IsNotNull().And(JobTable.Description.IsNotNull()));
			Assert.AreEqual(3, filter.Members.Count());

			filter = JobTable.Description.IsNotNull().And(JobTable.Description.IsNotNull()).And(JobTable.Description.IsNotNull());
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
