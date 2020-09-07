using NetORMLib.Columns;
using NetORMLib.Filters;
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
		ISelect<T> Where(params IFilter<T>[] Filters);
		ISelect<T> OrderBy(params IColumn<T>[] Columns);
		ISelect<T> Top(int Limit);
	}


}
