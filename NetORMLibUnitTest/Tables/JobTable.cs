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
	public class JobTable : Table
	{
		public static readonly IColumn<string> Description = new Column<string>();
		public static readonly IColumn<string> Company = new Column<string>() { IsNullable = true };
	}
}
