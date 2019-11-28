using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsLowerThanFilter<T, TVal>: ColumnFilter<T, TVal>, IIsLowerThanFilter<T, TVal>
	{
		public IsLowerThanFilter(IColumn<T, TVal> Column,TVal Value):base(Column,Value)
		{
		}

		public override string Format(string FormattedColumn, string FormattedParameter)
		{
			return $"{FormattedColumn}<{FormattedParameter}";
		}

		
	}
}
