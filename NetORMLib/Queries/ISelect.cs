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

		ISelect From(ITable Table);
		ISelect Where(params IFilter[] Filters);
		ISelect OrderBy(params IColumn[] Columns);
		ISelect OrderBy(OrderModes OrderMode, params IColumn[] Columns);
		ISelect Top(int Limit);
	}

	


}
