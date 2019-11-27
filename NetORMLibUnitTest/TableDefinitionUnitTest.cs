using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class TableDefinitionUnitTest
	{
		[TestMethod]
		public void ShouldReturnImplicitTableName()
		{
			Assert.AreEqual("Personn", TableDefinition<Personn>.Name);
		}
		[TestMethod]
		public void ShouldReturnExlicitTableName()
		{
			Assert.AreEqual("Personn", TableDefinition<PersonnExplicit>.Name);
		}
		[TestMethod]
		public void ShouldListColumns()
		{
			Assert.AreEqual(5, TableDefinition<Personn>.Columns.Count());
			Assert.AreEqual(Personn.PersonnID, TableDefinition<Personn>.Columns.ElementAt(0));
			Assert.AreEqual(Personn.FirstName, TableDefinition<Personn>.Columns.ElementAt(1));
			Assert.AreEqual(Personn.LastName, TableDefinition<Personn>.Columns.ElementAt(2));
			Assert.AreEqual(Personn.SecureCode, TableDefinition<Personn>.Columns.ElementAt(3));
			Assert.AreEqual(Personn.Job, TableDefinition<Personn>.Columns.ElementAt(4));

		}

		[TestMethod]
		public void ShouldGetColumnByName()
		{
			Assert.AreEqual(Personn.PersonnID, TableDefinition<Personn>.GetColumn("PersonnID"));
			Assert.AreEqual(Personn.FirstName, TableDefinition<Personn>.GetColumn("FirstName"));
		}
		[TestMethod]
		public void ShouldNotGetColumnByName()
		{
			Assert.ThrowsException<InvalidOperationException>(()=>TableDefinition<Personn>.GetColumn("InvalidColumnName"));
		}

	}
}
