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
	public abstract class BaseColumn<T,TVal>:IColumn<T,TVal>
	{
		public TVal DefaultValue
		{
			get;
			set;
		}
		private Dictionary<IRow<T>, TVal> dictionary;

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

		public abstract bool IsNullable
		{
			get;
		}

		

		public string Name
		{
			get;
			private set;
		}

		public string Table
		{
			get { return TableDefinition<T>.Name; }
		}

		public Type DataType
		{
			get { return typeof(TVal); }
		}

		

		public BaseColumn(string Name)
		{
			dictionary = new Dictionary<IRow<T>, TVal>();
			this.Name = Name;
			//this.IsNullable = Nullable.GetUnderlyingType(typeof(TVal))!=null;
		}

		object IColumn.GetValue(IRow Row)
		{
			return GetValue((IRow<T>)Row);
		}
		void IColumn.SetValue(IRow Row, object Value)
		{
			SetValue((IRow<T>)Row, (TVal)Value);
		}

		object IColumn<T>.GetValue(IRow<T> Row)
		{
			return GetValue(Row);
		}
		void IColumn<T>.SetValue(IRow<T> Row, object Value)
		{
			SetValue(Row, (TVal)Value);
		}


		public TVal GetValue(IRow<T> Row)
		{
			TVal result;
			if (dictionary.TryGetValue(Row, out result)) return result;
			return DefaultValue;
		}
		public void SetValue(IRow<T> Row, TVal Value)
		{
			dictionary[Row] = Value;
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
