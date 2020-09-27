using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateRelationUnitTest
	{
		
		[TestMethod]
		public void ShouldCreateRelationUsingExplicitParameters()
		{
			ICreateRelation query;

			query = new CreateRelation<int>(TestDB.PersonnTable,PersonnTable.PersonnID, JobTypeTable.JobTypeID) ;
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<int>(null, PersonnTable.PersonnID, JobTypeTable.JobTypeID));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<int>(TestDB.PersonnTable,null, JobTypeTable.JobTypeID));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateRelation<int>(TestDB.PersonnTable, PersonnTable.PersonnID, null));
		}



	}
}
