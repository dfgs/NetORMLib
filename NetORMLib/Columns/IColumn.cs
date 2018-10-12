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

	public interface IColumn<T>:IColumn
	{
		object GetValue(T Row);

	}

	public interface IColumn<T,TVal> : IColumn<T>
	{
		new TVal GetValue(T Row);

		IIsEqualToFilter<T, TVal> IsEqualTo(TVal Value);
		IIsNotEqualToFilter<T, TVal> IsNotEqualTo(TVal Value);
		IIsGreaterThanFilter<T, TVal> IsGreaterThan(TVal Value);
		IIsLowerThanFilter<T, TVal> IsLowerThan(TVal Value);
		IIsGreaterOrEqualToFilter<T, TVal> IsGreaterOrEqualTo(TVal Value);
		IIsLowerOrEqualToFilter<T, TVal> IsLowerOrEqualTo(TVal Value);
		IIsNullFilter<T> IsNull();
		IIsNotNullFilter<T> IsNotNull();
	}

}
