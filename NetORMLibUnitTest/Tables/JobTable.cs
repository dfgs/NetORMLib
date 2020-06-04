using NetORMLib;
using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetORMLibUnitTest.Tables
{
	public class JobTable
	{
		public static readonly IColumn<JobTable, string> Description = new Column<JobTable, string>();
		public static readonly IColumn<JobTable, string> Company = new Column<JobTable, string>() { IsNullable = true };
	}
}
