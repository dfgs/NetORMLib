using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class CreateColumnUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateColumnUsingExplicitParameters()
		{
			ICreateColumn query;

			query = new CreateColumn<Personn>(Personn.PersonnID);
			Assert.ThrowsException<ArgumentNullException>(() => new CreateColumn<Personn>(null));
		}



	}
}
