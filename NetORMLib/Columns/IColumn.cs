using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Columns
{
	public interface IColumn
	{
		string Name
		{
			get;
		}

		string Table
		{
			get;
		}
		
		object DefaultValue
		{
			get;
			set;
		}
		bool IsPrimaryKey
		{
			get;
			set;
		}

		bool IsNullable
		{
			get;
		}

		Type DataType
		{
			get;
		}

		bool IsIdentity
		{
			get;
			set;
		}
	}

	public interface IColumn<TVal>:IColumn
	{
		new TVal DefaultValue
		{
			get;
			set;
		}
	}

	public interface IColumn<T,TVal> : IColumn<TVal>
	{

		IIsEqualToFilter<TVal> IsEqualTo(TVal Value);
		IIsNotEqualToFilter<TVal> IsNotEqualTo(TVal Value);
		IIsGreaterThanFilter<TVal> IsGreaterThan(TVal Value);
		IIsLowerThanFilter<TVal> IsLowerThan(TVal Value);
		IIsGreaterOrEqualToFilter<TVal> IsGreaterOrEqualTo(TVal Value);
		IIsLowerOrEqualToFilter<TVal> IsLowerOrEqualTo(TVal Value);
		IIsNullFilter IsNull();
		IIsNotNullFilter IsNotNull();
	}

}
