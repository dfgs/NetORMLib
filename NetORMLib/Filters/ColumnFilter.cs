using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public abstract class ColumnFilter<TVal>:IColumnFilter<TVal>
	{
		private IColumn<TVal> column;
		IColumn IColumnFilter.Column => column;
		public IColumn<TVal> Column => column;

		private TVal value;
		object IColumnFilter.Value => value;
		public TVal Value => value;


		public ColumnFilter(IColumn<TVal> Column,TVal Value)
		{
			this.column = Column;this.value = Value;
		}

		public abstract string Format(string FormattedColumn, string FormattedParameter);

		public IAndFilter And(IFilter Filter)
		{
			if (Filter is IAndFilter other)
			{
				other.Add(this);
				return other;
			}
			else
			{
				return new AndFilter(this, Filter);
			}
		}

		public IOrFilter Or(IFilter Filter)
		{
			if (Filter is IOrFilter other)
			{
				other.Add(this);
				return other;
			}
			else
			{
				return new OrFilter(this, Filter);
			}
		}
	}
}
