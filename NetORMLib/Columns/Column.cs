using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
			get { return default(TVal); }
			//set { DefaultValue = (TVal)value; }
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
			private set;
		}

		//private TVal defaultValue;
		public TVal DefaultValue
		{
			get;
			set;
			/*get
			{
				return defaultValue;
			}
			set
			{
				defaultValue = value;HasDefaultValue = true;
			}*/
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

		
		

		public Column([CallerMemberName]string Name="empty")
		{
			Type type;
			FieldInfo fi;
			bool nullable;
			NullableAttribute attr;

			this.Name = Name;

			// temp method to know if TVal is nullable
			nullable = false;
			System.Diagnostics.StackTrace st;

			st = new System.Diagnostics.StackTrace();
			if (st.FrameCount >= 1)
			{
				type = st.GetFrame(1).GetMethod().DeclaringType;
				fi=type.GetField(Name);
				if (fi != null)
				{
					attr = fi.GetCustomAttribute<NullableAttribute>();
					if (attr != null) nullable = attr.Mode != 0;
				}
			}

				
			this.IsNullable = nullable || Nullable.GetUnderlyingType(typeof(TVal)) != null;
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
