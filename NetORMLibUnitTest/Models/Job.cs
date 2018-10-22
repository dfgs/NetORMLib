using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Models
{
	public class Job
	{
		public static readonly IColumn<Job, string> Description = new Column<Job, string>() {DefaultValue="New job" };
		public static readonly IColumn<Job, string> Company = new Column<Job, string>() { IsNullable=true };
	}
}
