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
	public class EnumTable : Table
	{
		public static readonly IColumn<int> EnumID = new Column<int>();
		public static readonly IColumn<string> Description = new Column<string>() { Constraint=ColumnConstraints.Unique };
	}
}
