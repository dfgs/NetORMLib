using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLog
	{

		public static readonly Column<UpgradeLog, DbInt> UpgradeLogID = new Column<UpgradeLog, DbInt>() { IsPrimaryKey = true, IsIdentity = true };
		public static readonly Column<UpgradeLog, DbDate> Date = new Column<UpgradeLog, DbDate>();
		public static readonly Column<UpgradeLog, DbInt> Revision = new Column<UpgradeLog, DbInt>();
	

	}
}
