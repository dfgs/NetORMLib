using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.DbTypes;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class DbTypeUnitTest
	{
		
		[TestMethod]
		public void ShouldNotAllowNullString()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new DbString(null));
		}



	}
}
