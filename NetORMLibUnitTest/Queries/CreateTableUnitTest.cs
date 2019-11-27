using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateTableUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateTableColumnsUsingExplicitParameters()
		{
			ICreateTable query;

			query = new CreateTable<Personn>(Personn.LastName, Personn.FirstName);
			Assert.AreEqual(2, query.Columns.Count());
			Assert.AreEqual(Personn.LastName, query.Columns.ElementAt(0));
			Assert.AreEqual(Personn.FirstName, query.Columns.ElementAt(1));
		}



	}
}
