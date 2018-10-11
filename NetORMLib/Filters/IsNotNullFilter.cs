using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNotNullFilter<T>:IFilter<T>, IIsNotNullFilter<T>
	{
		private IColumn<T> column;
		IColumn IIsNotNullFilter.Column => column;
		IColumn<T> IIsNotNullFilter<T>.Column => column;
		public IColumn<T> Column => column;



		public IsNotNullFilter(IColumn<T> Column)
		{
			this.column = Column;
		}

		public string Format(string FormattedColumn)
		{
			return $"{FormattedColumn} IS NOT NULL";
		}

		

		
	}
}
