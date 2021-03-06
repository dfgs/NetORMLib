﻿using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public interface ISingleTable:ITable
	{
		string Name
		{
			get;
		}

		IJoin On(IColumn FirstColumn, IColumn SecondColumn);

	}
}
