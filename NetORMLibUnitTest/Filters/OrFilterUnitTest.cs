using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Filters
{
	[TestClass]
	public class OrToFilterUnitTest
	{
		
		[TestMethod]
		public void ShouldFormat()
		{
			IOrFilter filter;

			filter = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			Assert.AreEqual(2, filter.Members.Count());
			Assert.AreEqual("(C1 OR C2)", filter.Format(new string[] {"C1","C2" }));
		}

		[TestMethod]
		public void ShouldCreateOrFilter()
		{
			IOrFilter or;
			IOrFilter filter;

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.Or(JobTable.Description.IsNull());
			Assert.AreEqual(3,filter.Members.Count());

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.Or(JobTable.Description.IsNull().Or(JobTable.Description.IsNull()));
			Assert.AreEqual(4, filter.Members.Count());

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.Or(JobTable.Description.IsNull()).Or(JobTable.Description.IsNull());
			Assert.AreEqual(4, filter.Members.Count());
		}

		[TestMethod]
		public void ShouldCreateAndFilter()
		{
			IOrFilter or;
			IAndFilter filter;

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.And(JobTable.Description.IsNull());
			Assert.AreEqual(2, filter.Members.Count());

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.And(JobTable.Description.IsNull().And(JobTable.Description.IsNull()));
			Assert.AreEqual(3, filter.Members.Count());

			or = new OrFilter(JobTable.Description.IsEqualTo("test"), JobTable.Description.IsEqualTo("test"));
			filter = or.And(JobTable.Description.IsNull()).And(JobTable.Description.IsNull());
			Assert.AreEqual(3, filter.Members.Count());
		}
	}
}
