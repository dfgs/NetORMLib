using System;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Sql;
using NetORMLib.Sql.CommandBuilders;
using NetORMLibUnitTest.Models;

namespace NetORMLibUnitTest
{
	[TestClass]
	public class SqlCreateTableCommandBuilderUnitTest
	{

		[TestMethod]
		public void ShouldThrowExceptionIfNoColumnSpecified()
		{
			ICommandBuilder builder;

			builder = new SqlCommandBuilder();
			Assert.ThrowsException<ArgumentNullException>( () => new CreateTable<Personn>());
			
		}

		[TestMethod]
		public void ShouldBuildExplicitCreateTable()
		{
			IQuery query;
			ICommandBuilder builder;
			DbCommand command;

			query = new CreateTable<Personn>(Personn.FirstName, Personn.LastName);
			builder = new SqlCommandBuilder();
			command = builder.BuildCommand(query);
			Assert.AreEqual("CREATE TABLE [Personn] ([Personn].[FirstName]=@FirstName0, [Personn].[LastName]=@LastName1", command.CommandText);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("@FirstName0", command.Parameters[0].ParameterName);
			Assert.AreEqual("John", command.Parameters[0].Value);
			Assert.AreEqual("@LastName1", command.Parameters[1].ParameterName);
			Assert.AreEqual("Doe", command.Parameters[1].Value);
		}
		/*
		 * 
		 protected override SqlCommand OnCreateTableCreateCommand(ITable Table)
		{
			string sql;


			sql = "create table [" + Table.Name + "] (";
			foreach (IColumn column in Table.Columns.Where(item => (item.Revision == 0 ) && !item.IsVirtual))
			{
				sql += OnFormatColumnName(column) + " " + GetTypeName(column) + (column.IsNullable ? " NULL," : " NOT NULL,");
			}
			sql += "CONSTRAINT [PK_" + Table.Name + "]" + " PRIMARY KEY CLUSTERED ([" + Table.PrimaryKey.Name + "] ASC))";

			return new SqlCommand(sql);
		}*/


	
	

	}
}
