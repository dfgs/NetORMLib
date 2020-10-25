using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNotNullFilter : IIsNotNullFilter
	{
		private IColumn column;
		public IColumn Column => column;



		public IsNotNullFilter(IColumn Column)
		{
			this.column = Column;
		}

		public string Format(string FormattedColumn)
		{
			return $"{FormattedColumn} IS NOT NULL";
		}

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
