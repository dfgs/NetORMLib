using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class EqualsFilter<T,TVal>: ColumnFilter<T,TVal>, IEqualsFilter<T,TVal>
	{
		


		public EqualsFilter(IColumn<T, TVal> Column,TVal Value):base(Column,Value)
		{
		}

		public override string Format(string FormattedColumn, string FormattedParameter)
		{
			return $"{FormattedColumn}={FormattedParameter}";
		}

		
	}
}
