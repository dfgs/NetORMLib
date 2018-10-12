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
		object GetValue(object Row);
	}

	public interface IColumn<TVal>:IColumn
	{

	}

	public interface IColumn<T,TVal> : IColumn<TVal>
	{
		TVal GetValue(T Row);

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
