using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IIsGreaterOrEqualToFilter<T, TVal>:IColumnFilter<T, TVal>
		where TVal: IDbType
	{
	}

	
}
