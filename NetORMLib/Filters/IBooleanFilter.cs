using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IBooleanFilter:IFilter
	{
		IEnumerable<IFilter> Members
		{
			get;
		}
		string Format(IEnumerable<string> FormattedMembers);

	}

	public interface IBooleanFilter<T>:IBooleanFilter,IFilter<T>
	{
		new IEnumerable<IFilter<T>> Members
		{
			get;
		}
	}

}
