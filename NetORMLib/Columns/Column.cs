using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Filters;

namespace NetORMLib.Columns
{
	public class Column<T,TVal>:IColumn<T,TVal>
	{
		public string Name
		{
			get;
			private set;
		}

		public Column([CallerMemberName]string Name=null)
		{
			this.Name = Name;
		}


		public override string ToString()
		{
			return Name;
		}

		public IEqualsFilter<T, TVal> Equals(TVal Value)
		{
			return new EqualsFilter<T,TVal>(this, Value);
		}


	}
}
