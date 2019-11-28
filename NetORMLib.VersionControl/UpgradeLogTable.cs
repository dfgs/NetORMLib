using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLogTable
	{

		public static readonly Column<UpgradeLogTable, int> UpgradeLogID = new Column<UpgradeLogTable, int>() { IsPrimaryKey = true, IsIdentity = true };
		public static readonly Column<UpgradeLogTable, DateTime> Date = new Column<UpgradeLogTable, DateTime>();
		public static readonly Column<UpgradeLogTable, int> Revision = new Column<UpgradeLogTable, int>();
	

	}
}
