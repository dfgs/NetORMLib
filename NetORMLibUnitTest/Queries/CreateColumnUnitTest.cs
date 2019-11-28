using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateColumnUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateColumnUsingExplicitParameters()
		{
			ICreateColumn query;

			query = new CreateColumn<PersonnTable>(PersonnTable.PersonnID);
			Assert.ThrowsException<ArgumentNullException>(() => new CreateColumn<PersonnTable>(null));
		}



	}
}
