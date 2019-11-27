using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class InsertUnitTest
	{
		[TestMethod]
		public void ShouldInsert()
		{
			IInsert query;

			query = new Insert<Personn>();
		}

		[TestMethod]
		public void ShouldInsertColumnsUsingExplicitParameters()
		{
			IInsert query;

			query = new Insert<Personn>( ).Set(Personn.FirstName,"John").Set(Personn.LastName,"Doe");
			Assert.AreEqual(2, query.Setters.Count());
			Assert.AreEqual(Personn.FirstName, query.Setters.ElementAt(0).Column);
			Assert.AreEqual(Personn.LastName, query.Setters.ElementAt(1).Column);
		}

		


	}
}
