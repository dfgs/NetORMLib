using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISelect
	{

	}
	public interface ISelect<T> : ISelect, IQuery<T>, IFilterableQuery<T>, IOrderableQuery<T>
	{
		IEnumerable<IColumn<T>> Columns
		{
			get;
		}

		IEnumerable<IColumn<T>> Orders
		{
			get;
		}

		IEnumerable<IFilter<T>> Filters
		{
			get;
		}


	}
}
