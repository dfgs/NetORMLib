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

		public string Table
		{
			get { return Table<T>.Name; }
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

		public IIsEqualToFilter<T, TVal> IsEqualTo(TVal Value)
		{
			return new IsEqualToFilter<T, TVal>(this, Value);
		}
		public IIsNotEqualToFilter<T, TVal> IsNotEqualTo(TVal Value)
		{
			return new IsNotEqualToFilter<T, TVal>(this, Value);
		}
		public IIsGreaterThanFilter<T, TVal> IsGreaterThan(TVal Value)
		{
			return new IsGreaterThanFilter<T, TVal>(this, Value);
		}
		public IIsLowerThanFilter<T, TVal> IsLowerThan(TVal Value)
		{
			return new IsLowerThanFilter<T, TVal>(this, Value);
		}
		public IIsGreaterOrEqualToFilter<T, TVal> IsGreaterOrEqualTo(TVal Value)
		{
			return new IsGreaterOrEqualToFilter<T, TVal>(this, Value);
		}
		public IIsLowerOrEqualToFilter<T, TVal> IsLowerOrEqualTo(TVal Value)
		{
			return new IsLowerOrEqualToFilter<T, TVal>(this, Value);
		}
		public IIsNullFilter<T> IsNull()
		{
			return new IsNullFilter<T>(this);
		}
		public IIsNotNullFilter<T> IsNotNull()
		{
			return new IsNotNullFilter<T>(this);
		}

		public override string ToString()
		{
			return Name;
		}



	}
}
