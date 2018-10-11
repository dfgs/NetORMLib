using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IColumnFilter:IFilter
	{
		IColumn Column
		{
			get;
		}

		object Value
		{
			get;
		}

		string Format(string FormattedColumn, string FormattedParameter);
	}

	public interface IColumnFilter<T> : IColumnFilter,IFilter<T>
	{
		new IColumn<T> Column
		{
			get;
		}
	}

	public interface IColumnFilter<T, TVal> : IColumnFilter<T>
	{
		new IColumn<T, TVal> Column
		{
			get;
		}
		new TVal Value
		{
			get;
		}

	}



}
