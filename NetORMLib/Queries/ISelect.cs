using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISelect:IQuery, IOrderableQuery,IFilterableQuery
	{
		IEnumerable<IColumn> Columns
		{
			get;
		}

		IEnumerable<IColumn> Orders
		{
			get;
		}

		IEnumerable<IFilter> Filters
		{
			get;
		}

		ISelect From<T>();
		new ISelect OrderBy(params IColumn[] Columns);
		new ISelect Where(params IFilter[] Filters);

	}

}
