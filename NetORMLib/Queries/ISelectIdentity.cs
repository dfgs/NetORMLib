using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISelectIdentity: IQuery
	{

	}

	public interface ISelectIdentity<T>:ISelectIdentity
	{
	}


}
