using NetORMLib.Columns;
using NetORMLib.DbTypes;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IInsert:IQuery
	{
		IEnumerable<ISetter> Setters
		{
			get;
		}

	}

	public interface IInsert<T>:IInsert
	{
		new IEnumerable<ISetter<T>> Setters
		{
			get;
		}

		IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal: IDbType;

	}

}
