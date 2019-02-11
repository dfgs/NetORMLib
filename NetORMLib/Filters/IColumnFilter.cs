using NetORMLib.Columns;
using NetORMLib.DbTypes;
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

		IDbType Value
		{
			get;
		}

		string Format(string FormattedColumn, string FormattedParameter);
	}

	
	public interface IColumnFilter<T> : IColumnFilter,IFilter<T>
	{
		new IColumn<T> Column
		{
			get;
		}
	}

	public interface IColumnFilter<T,TVal> : IColumnFilter<T>
		where TVal: IDbType
	{
		new IColumn<T,TVal> Column
		{
			get;
		}
		new TVal Value
		{
			get;
		}
	}


}
