using NetORMLib;
using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetORMLibUnitTest.Models
{
	public class Job
	{
		public static readonly IColumn<Job, DbString> Description = new Column<Job, DbString>();
		public static readonly IColumn<Job, DbString> Company = new NullableColumn<Job, DbString>();
	}
}
