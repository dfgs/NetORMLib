using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNotEqualToFilter<TVal>: ColumnFilter<TVal>, IIsNotEqualToFilter<TVal>
	{

		public IsNotEqualToFilter(IColumn<TVal> Column,TVal Value):base(Column,Value)
		{
		}

		public override string Format(string FormattedColumn, string FormattedParameter)
		{
			return $"{FormattedColumn}<>{FormattedParameter}";
		}

		
	}
}
