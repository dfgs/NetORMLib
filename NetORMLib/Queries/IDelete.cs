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
	public interface IDelete:IFilterableQuery
	{
		IDelete From(ITable Table);
		IDelete Where(params IFilter[] Filters);

	}
	

}
