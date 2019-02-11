using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.DbTypes;
using NetORMLib.Queries;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class CreateRelationUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateRelationUsingExplicitParameters()
		{
			ICreateRelation query;

			query = new CreateRelation<Personn, JobType, DbInt>(Personn.PersonnID, JobType.JobTypeID);
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<Personn, JobType, DbInt>(null, JobType.JobTypeID));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<Personn, JobType, DbInt>(Personn.PersonnID, null));
		}



	}
}
