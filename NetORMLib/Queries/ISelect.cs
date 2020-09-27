using NetORMLib.Columns;
using NetORMLib.Filters;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISelect: IOrderableQuery,IFilterableQuery
	{
		int Limit
		{
			get;
		}
		IEnumerable<IColumn> Columns
		{
			get;
		}

	}

	public interface ISelect<T>:ISelect,IOrderableQuery<T>,IFilterableQuery<T>
	{
		new IEnumerable<IColumn<T>> Columns
		{
			get;
		}
		ISelect<T> From(ITable Table);
		ISelect<T> Where(params IFilter<T>[] Filters);
		ISelect<T> OrderBy(params IColumn<T>[] Columns);
		ISelect<T> OrderBy(OrderModes OrderMode,params IColumn<T>[] Columns);
		ISelect<T> Top(int Limit);
	}


}
