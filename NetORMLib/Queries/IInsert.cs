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
		IInsert Into(ITable Table);

		IInsert Set<TVal>(IColumn<TVal> Column, TVal? Value)
			where TVal : struct;
		IInsert Set<TVal>(IColumn<TVal> Column, TVal Value)
			where TVal : class;
	}


}
