using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISelect:IQuery
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
	}
	public interface ISelect<T> : ISelect, IQuery<T>, IFilterableQuery<T>, IOrderableQuery<T>
	{
		new IEnumerable<IColumn<T>> Columns
		{
			get;
		}

		new IEnumerable<IColumn<T>> Orders
		{
			get;
		}

		new IEnumerable<IFilter<T>> Filters
		{
			get;
		}


	}
}
