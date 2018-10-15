using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IUpdate:IQuery, IFilterableQuery
	{

		IEnumerable<ISetter> Setters
		{
			get;
		}


		IEnumerable<IFilter> Filters
		{
			get;
		}

		IUpdate Set<T, TVal>(IColumn<T, TVal> Column, TVal Value);
		IUpdate Set<T, TVal>(IEnumerable<ISetter<T,TVal>> Setters);
		new IUpdate Where(params IFilter[] Filters);

	}

}
