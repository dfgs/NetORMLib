using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLibUnitTest.Tables;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class TableDefinitionUnitTest
	{
		[TestMethod]
		public void ShouldReturnImplicitTableName()
		{
			Assert.AreEqual("Personn", TableDefinition<PersonnTable>.Name);
		}
		[TestMethod]
		public void ShouldReturnExlicitTableName()
		{
			Assert.AreEqual("Personn", TableDefinition<PersonnExplicitName>.Name);
		}
		[TestMethod]
		public void ShouldReturnCustomTableName()
		{
			Assert.AreEqual("PersonnTestName", TableDefinition<PersonnTestName>.Name);
		}
		[TestMethod]
		public void ShouldListColumns()
		{
			Assert.AreEqual(5, TableDefinition<PersonnTable>.Columns.Count());
			Assert.AreEqual(PersonnTable.PersonnID, TableDefinition<PersonnTable>.Columns.ElementAt(0));
			Assert.AreEqual(PersonnTable.FirstName, TableDefinition<PersonnTable>.Columns.ElementAt(1));
			Assert.AreEqual(PersonnTable.LastName, TableDefinition<PersonnTable>.Columns.ElementAt(2));
			Assert.AreEqual(PersonnTable.SecureCode, TableDefinition<PersonnTable>.Columns.ElementAt(3));
			Assert.AreEqual(PersonnTable.Job, TableDefinition<PersonnTable>.Columns.ElementAt(4));

		}

		[TestMethod]
		public void ShouldGetColumnByName()
		{
			Assert.AreEqual(PersonnTable.PersonnID, TableDefinition<PersonnTable>.GetColumn("PersonnID"));
			Assert.AreEqual(PersonnTable.FirstName, TableDefinition<PersonnTable>.GetColumn("FirstName"));
		}
		[TestMethod]
		public void ShouldNotGetColumnByName()
		{
			Assert.ThrowsException<InvalidOperationException>(()=>TableDefinition<PersonnTable>.GetColumn("InvalidColumnName"));
		}

	}
}
