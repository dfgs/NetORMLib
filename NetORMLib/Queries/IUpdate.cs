using NetORMLib.Columns;

using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IUpdate:IFilterableQuery
	{
		IEnumerable<ISetter> Setters
		{
			get;
		}
	}

	public interface IUpdate<T>:IUpdate,IFilterableQuery<T>
	{
		new IEnumerable<ISetter<T>> Setters
		{
			get;
		}

		IUpdate<T> Where(params IFilter<T>[] Filters);
		IUpdate<T> Set<TVal>(IColumn<T, TVal> Column, TVal? Value)
			where TVal : struct;
		IUpdate<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal : class;
	}

}
