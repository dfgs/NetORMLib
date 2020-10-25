using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class AndToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IAndFilter filter;

			filter = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			Assert.AreEqual(2, filter.Members.Count());
			Assert.AreEqual("(C1 AND C2)", filter.Format(new string[] {"C1","C2" }));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IAndFilter and;
			IOrFilter filter;

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.Or(JobTable.Description.IsNull());
			Assert.AreEqual(2,filter.Members.Count());

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.Or(JobTable.Description.IsNull().Or(JobTable.Description.IsNull()));
			Assert.AreEqual(3, filter.Members.Count());

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.Or(JobTable.Description.IsNull()).Or(JobTable.Description.IsNull());
			Assert.AreEqual(3, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IAndFilter and;
			IAndFilter filter;

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.And(JobTable.Description.IsNull());
			Assert.AreEqual(3, filter.Members.Count());

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.And(JobTable.Description.IsNull().And(JobTable.Description.IsNull()));
			Assert.AreEqual(4, filter.Members.Count());

			and = new AndFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = and.And(JobTable.Description.IsNull()).And(JobTable.Description.IsNull());
			Assert.AreEqual(4, filter.Members.Count());
		}
	}
}
