using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class EqualsFilter<T,TVal>:IEqualsFilter<T,TVal>
	{
		public IColumn<T,TVal> Column
		{
			get;
			private set;
		}

		public TVal Value
		{
			get;
			private set;
		}
		public EqualsFilter(IColumn<T, TVal> Column,TVal Value)
		{
			this.Column = Column;this.Value = Value;
		}
	}
}
