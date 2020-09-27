using NetORMLib.Columns;

using NetORMLib.Filters;
using NetORMLib.Tables;
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
		IInsert<T> Into(ITable Table);

		IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal? Value)
			where TVal : struct;
		IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal : class;

	}

}
