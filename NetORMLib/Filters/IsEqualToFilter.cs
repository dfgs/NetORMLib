using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsEqualToFilter<TVal>: ColumnFilter<TVal>, IIsEqualToFilter<TVal>
	{
		


		public IsEqualToFilter(IColumn<TVal> Column,TVal Value):base(Column,Value)
		{
		}

		public override string Format(string FormattedColumn, string FormattedParameter)
		{
			return $"{FormattedColumn}={FormattedParameter}";
		}

		
	}
}
