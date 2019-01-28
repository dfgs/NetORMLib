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
		object IColumn.DefaultValue
		{
			get { return DefaultValue; }
			set { DefaultValue = (TVal)value; }
		}

		public bool IsPrimaryKey
		{
			get;
			set;
		}
		public bool IsIdentity
		{
			get;
			set;
		}

		public bool IsNullable
		{
			get;
			set;
		}

		public bool HasDefaultValue
		{
			get;
			private set;
		}

		private TVal defaultValue;
		public TVal DefaultValue
		{
			get
			{
				return defaultValue;
			}
			set
			{
				defaultValue = value;HasDefaultValue = true;
			}
		}

		public string Name
		{
			get;
			private set;
		}

		public string Table
		{
			get { return Table<T>.Name; }
		}

		public Type DataType
		{
			get { return typeof(TVal); }
		}

		public Column([CallerMemberName]string Name=null)
		{
			this.Name = Name;
		}


		

		public IIsEqualToFilter<T,TVal> IsEqualTo(TVal Value)
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
