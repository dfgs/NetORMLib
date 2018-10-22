using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IDelete:IFilterableQuery
	{
		
	}
	public interface IDelete<T>:IDelete,IFilterableQuery<T>
	{
		IDelete<T> Where(params IFilter<T>[] Filters);

	}


}
