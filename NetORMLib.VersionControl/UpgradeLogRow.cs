﻿using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public class UpgradeLogRow
	{
		public int UpgradeLogID
		{
			get;
			set;
		}
		public DateTime Date
		{
			get;
			set;
		}
		public int Revision
		{
			get;
			set;
		}
	

	}
}