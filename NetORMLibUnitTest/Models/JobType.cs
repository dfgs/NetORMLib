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
	public class JobType
	{
		public static readonly IColumn<JobType, DbInt> JobTypeID = new Column<JobType, DbInt>() { IsPrimaryKey=true };
		public static readonly IColumn<JobType, DbString> Name = new Column<JobType, DbString>();
		public static readonly IColumn<JobType, DbString> Description = new Column<JobType, DbString>() ;
	}
}
