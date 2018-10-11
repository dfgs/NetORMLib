using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IAndFilter:IBooleanFilter
	{
	}
	public interface IAndFilter<T>:IBooleanFilter<T>
	{
	}
}
