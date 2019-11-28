﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class UpdateUnitTest
	{
		
		[TestMethod]
		public void ShouldUpdate()
		{
			IUpdate query;

			query = new Update<Personn>().Set(Personn.FirstName,"John");
			Assert.AreEqual("Personn", query.Table);
			Assert.AreEqual(1, query.Setters.Count());
		}

		
		[TestMethod]
		public void ShouldFilterByColumns()
		{
			IUpdate query;

			query = new Update<Personn>().Set(Personn.FirstName, "John").Where(Personn.FirstName.IsEqualTo("Homer"));
			Assert.AreEqual("Personn", query.Table);
			Assert.AreEqual(1, query.Setters.Count());
			Assert.AreEqual(1, query.Filters.Count());
		}


	}
}