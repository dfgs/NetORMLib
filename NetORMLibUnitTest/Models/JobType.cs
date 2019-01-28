using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Models
{
	public class JobType
	{
		public static readonly IColumn<JobType, int> JobTypeID = new Column<JobType, int>() { IsPrimaryKey=true };
		public static readonly IColumn<JobType, string> Name = new Column<JobType, string>() { DefaultValue = "New job" };
		public static readonly IColumn<JobType, string> Description = new Column<JobType, string>() ;
	}
}
