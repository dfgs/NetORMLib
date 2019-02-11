﻿using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNotEqualToFilter<T, TVal>: ColumnFilter<T, TVal>, IIsNotEqualToFilter<T, TVal>
		where TVal: IDbType
	{

		public IsNotEqualToFilter(IColumn<T, TVal> Column,TVal Value):base(Column,Value)
		{
		}

		public override string Format(string FormattedColumn, string FormattedParameter)
		{
			return $"{FormattedColumn}<>{FormattedParameter}";
		}

		
	}
}
