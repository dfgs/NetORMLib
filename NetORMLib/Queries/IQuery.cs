﻿using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IQuery
	{
		ITable Table
		{
			get;
		}
	}
	
	

}
