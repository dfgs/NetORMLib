﻿using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IFilterableQuery:IQuery
	{
		IEnumerable<IFilter> Filters
		{
			get;
		}

	}

	public interface IFilterableQuery<T>: IQuery<T>,IFilterableQuery
	{
		new IEnumerable<IFilter<T>> Filters
		{
			get;
		}

	}


}
