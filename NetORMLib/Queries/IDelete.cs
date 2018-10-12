using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IDelete:IQuery, IFilterableQuery
	{

		IEnumerable<IFilter> Filters
		{
			get;
		}

		IDelete From<T>();
		new IDelete Where(params IFilter[] Filters);

	}

}
