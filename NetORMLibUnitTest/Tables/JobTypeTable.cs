using NetORMLib;
using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Tables
{
	public class JobTypeTable
	{
		public static readonly IColumn<JobTypeTable, int> JobTypeID = new Column<JobTypeTable, int>() { IsPrimaryKey=true };
		public static readonly IColumn<JobTypeTable, string> Name = new Column<JobTypeTable, string>();
		public static readonly IColumn<JobTypeTable, string> Description = new Column<JobTypeTable, string>() ;
	}
}
