using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNullFilter<T> : IIsNullFilter<T>
	{
		private IColumn<T> column;
		IColumn IIsNullFilter.Column => column;
		public IColumn<T> Column => column;



		public IsNullFilter(IColumn<T> Column)
		{
			this.column = Column;
		}

		public string Format(string FormattedColumn)
		{
			return $"{FormattedColumn} IS NULL";
		}

		

		
	}
}
