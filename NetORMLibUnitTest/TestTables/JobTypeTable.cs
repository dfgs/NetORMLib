using NetORMLib;
using NetORMLib.Columns;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Tables
{
	public class JobTypeTable : Table
	{
		public static readonly IColumn<int> JobTypeID = new Column<int>() { Constraint = ColumnConstraints.PrimaryKey };
		public static readonly IColumn<string> Name = new Column<string>();
		public static readonly IColumn<string> Description = new Column<string>() ;
	}
}
