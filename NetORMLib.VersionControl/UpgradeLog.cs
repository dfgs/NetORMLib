using NetORMLib.Columns;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLog
	{
		public int UpgradeLogID { get; set; }
		public DateTime Date { get; set; }
		public int Revision { get; set; }


	}
}
