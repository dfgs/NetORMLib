using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NetORMLib.Filters;

namespace NetORMLib.Columns
{
	public class Column<T,TVal>:IColumn<T,TVal>
	{
		private Dictionary<T, TVal> values;

		private static Regex nameRegex = new Regex("(.+)Column");
		public string Name
		{
			get;
			private set;
		}

		public Column([CallerMemberName]string Name=null)
		{
			Match match;
			match = nameRegex.Match(Name);
			if (match.Success)
			{
				this.Name = match.Groups[1].Value;
			}
			else
			{
				this.Name = Name;
			}

			values = new Dictionary<T, TVal>();
		}


		object IColumn.GetValue(object Row)
		{
			return GetValue((T)Row);
		}
		object IColumn<T>.GetValue(T Row)
		{
			return GetValue((T)Row);
		}
		public TVal GetValue(T Row)
		{
			TVal value;

			values.TryGetValue(Row, out value);
			return value;
		}

		public IEqualsFilter<T, TVal> Equals(TVal Value)
		{
			return new EqualsFilter<T,TVal>(this, Value);
		}
		public override string ToString()
		{
			return Name;
		}



	}
}
