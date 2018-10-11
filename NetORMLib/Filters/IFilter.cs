using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IFilter<T>
	{
	}

	public interface IFilter<T,TVal>:IFilter<T>
	{
	}
}
