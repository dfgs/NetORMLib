﻿using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public interface IJoin
	{
		ISingleTable OtherTable
		{
			get;
		}
		IColumn FirstColumn
		{
			get;
		}
		IColumn SecondColumn
		{
			get;
		}
	}
}
