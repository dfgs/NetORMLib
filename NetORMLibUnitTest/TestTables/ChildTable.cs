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
	public class ChildTable : Table
	{
		public static readonly IColumn<int> ChildID = new Column<int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<string> FirstName = new Column<string>();
		public static readonly IColumn<string> LastName = new Column<string>();
		public static readonly IColumn<int> PersonnID = new Column<int>() ;
	}
}
