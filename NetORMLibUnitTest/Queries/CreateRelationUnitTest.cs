using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateRelationUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateRelationUsingExplicitParameters()
		{
			ICreateRelation query;

			query = new CreateRelation<Personn, JobType, int>(Personn.PersonnID, JobType.JobTypeID);
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<Personn, JobType, int>(null, JobType.JobTypeID));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<Personn, JobType, int>(Personn.PersonnID, null));
		}



	}
}
