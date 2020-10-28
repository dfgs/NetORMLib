using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLib.Queries;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest.Queries
{
	[TestClass]
	public class CreateConstraintUnitTest
	{
		
		[TestMethod]
		public void ShouldCreatePrimaryKeyConstraint()
		{
			ICreateConstraint query;


			Assert.ThrowsException<ArgumentNullException>(() => new CreateConstraint(null, ColumnConstraints.PrimaryKey, PersonnTable.PersonnID));
			Assert.ThrowsException<ArgumentNullException>(() => new CreateConstraint(TestDB.PersonnTable, ColumnConstraints.PrimaryKey, null));

			query = new CreateConstraint(TestDB.PersonnTable, ColumnConstraints.PrimaryKey, PersonnTable.PersonnID);
			Assert.AreEqual(TestDB.PersonnTable, query.Table);
			Assert.AreEqual(ColumnConstraints.PrimaryKey, query.Constraint);
			Assert.AreEqual(1, query.Columns.Count());
		}



	}
}
