using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class IsNullFilter : IIsNullFilter
	{
		private IColumn column;
		public IColumn Column => column;



		public IsNullFilter(IColumn Column)
		{
			this.column = Column;
		}

		public string Format(string FormattedColumn)
		{
			return $"{FormattedColumn} IS NULL";
		}

		

		
	}
}
