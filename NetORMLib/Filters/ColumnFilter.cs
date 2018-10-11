using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public abstract class ColumnFilter<T,TVal>:IColumnFilter<T,TVal>
	{
		private IColumn<T, TVal> column;
		IColumn IColumnFilter.Column => column;
		IColumn<T> IColumnFilter<T>.Column => column;
		public IColumn<T, TVal> Column => column;

		private TVal value;
		object IColumnFilter.Value => value;
		public TVal Value => value;



		public ColumnFilter(IColumn<T, TVal> Column,TVal Value)
		{
			this.column = Column;this.value = Value;
		}

		public abstract string Format(string FormattedColumn, string FormattedParameter);
		

		
	}
}
