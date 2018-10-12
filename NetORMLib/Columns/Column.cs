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
		
		public TVal GetValue(T Row)
		{
			TVal value;

			values.TryGetValue(Row, out value);
			return value;
		}

		public IIsEqualToFilter<TVal> IsEqualTo(TVal Value)
		{
			return new IsEqualToFilter<TVal>(this, Value);
		}
		public IIsNotEqualToFilter<TVal> IsNotEqualTo(TVal Value)
		{
			return new IsNotEqualToFilter<TVal>(this, Value);
		}
		public IIsGreaterThanFilter<TVal> IsGreaterThan(TVal Value)
		{
			return new IsGreaterThanFilter<TVal>(this, Value);
		}
		public IIsLowerThanFilter<TVal> IsLowerThan(TVal Value)
		{
			return new IsLowerThanFilter<TVal>(this, Value);
		}
		public IIsGreaterOrEqualToFilter<TVal> IsGreaterOrEqualTo(TVal Value)
		{
			return new IsGreaterOrEqualToFilter<TVal>(this, Value);
		}
		public IIsLowerOrEqualToFilter<TVal> IsLowerOrEqualTo(TVal Value)
		{
			return new IsLowerOrEqualToFilter<TVal>(this, Value);
		}
		public IIsNullFilter IsNull()
		{
			return new IsNullFilter(this);
		}
		public IIsNotNullFilter IsNotNull()
		{
			return new IsNotNullFilter(this);
		}

		public override string ToString()
		{
			return Name;
		}



	}
}
