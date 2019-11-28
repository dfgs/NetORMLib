using NetORMLib;
using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace NetORMLibUnitTest.Tables
{
	public class PersonnTable
	{
		public static readonly IColumn<PersonnTable, int> PersonnID = new Column<PersonnTable, int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<PersonnTable, string> FirstName = new Column<PersonnTable, string>();
		public static readonly IColumn<PersonnTable, string> LastName = new Column<PersonnTable, string>();
		public static readonly IColumn<PersonnTable, int> SecureCode = new NullableColumn<PersonnTable, int>();
		public static readonly IColumn<PersonnTable, string> Job = new NullableColumn<PersonnTable, string>() { DefaultValue="No job"};
	}
}
