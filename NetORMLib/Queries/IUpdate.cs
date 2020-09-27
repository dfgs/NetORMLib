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
		IUpdate Where(params IFilter[] Filters);
		IUpdate Set<TVal>(IColumn<TVal> Column, TVal? Value)
			where TVal : struct;
		IUpdate Set<TVal>(IColumn<TVal> Column, TVal Value)
			where TVal : class;
	}

	

}
