using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLog
	{

		public static readonly Column<UpgradeLog, int> UpgradeLogID = new Column<UpgradeLog, int>() { IsPrimaryKey = true, IsIdentity = true };
		public static readonly Column<UpgradeLog, DateTime> Date = new Column<UpgradeLog, DateTime>();
		public static readonly Column<UpgradeLog, int> Revision = new Column<UpgradeLog, int>();
	

	}
}
