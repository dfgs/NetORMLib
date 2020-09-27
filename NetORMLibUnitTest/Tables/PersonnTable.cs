using NetORMLib;
using NetORMLib.Columns;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace NetORMLibUnitTest.Tables
{
	public class PersonnTable : Table
	{
		public static readonly IColumn<PersonnTable, int> PersonnID = new Column<PersonnTable, int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<PersonnTable, string> FirstName = new Column<PersonnTable, string>();
		public static readonly IColumn<PersonnTable, string> LastName = new Column<PersonnTable, string>();
		public static readonly IColumn<PersonnTable, int> SecureCode = new Column<PersonnTable, int>() { IsNullable=true};
		public static readonly IColumn<PersonnTable, string> Job = new Column<PersonnTable, string>() { DefaultValue="No job",IsNullable=true};
	}
}
