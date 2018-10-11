using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IIsNullFilter:IFilter
	{
		IColumn Column
		{
			get;
		}

		string Format(string FormattedColumn);
	}

	public interface IIsNullFilter<T> : IIsNullFilter,IFilter<T>
	{
		new IColumn<T> Column
		{
			get;
		}
	}

	



}
