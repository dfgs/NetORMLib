using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IOrFilter:IBooleanFilter
	{
	}
	public interface IOrFilter<T>:IBooleanFilter<T>,IOrFilter
	{
	}
}
