using NetORMLib.Columns;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLogTable:Table
	{

		public static readonly Column<int> UpgradeLogID = new Column<int>() { IsPrimaryKey = true, IsIdentity = true };
		public static readonly Column<DateTime> Date = new Column<DateTime>();
		public static readonly Column<int> Revision = new Column<int>();
	

	}
}
