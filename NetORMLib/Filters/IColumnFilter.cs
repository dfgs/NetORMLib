using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public interface IColumnFilter:IFilter
	{
		IColumn Column
		{
			get;
		}

		object Value
		{
			get;
		}

		string Format(string FormattedColumn, string FormattedParameter);
	}

	
	public interface IColumnFilter<TVal> : IColumnFilter
	{
		new IColumn<TVal> Column
		{
			get;
		}
		new TVal Value
		{
			get;
		}
	}

	


}
