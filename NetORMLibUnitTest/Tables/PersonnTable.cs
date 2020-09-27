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
		public static readonly IColumn<int> PersonnID = new Column<int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<string> FirstName = new Column<string>();
		public static readonly IColumn<string> LastName = new Column<string>();
		public static readonly IColumn<int> SecureCode = new Column<int>() { IsNullable=true};
		public static readonly IColumn<string> Job = new Column<string>() { DefaultValue="No job",IsNullable=true};
	}
}
