using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ICreateRelation:IQuery
	{
		IColumn PrimaryColumn
		{
			get;
		}
		IColumn ForeignColumn
		{
			get;
		}
	}

	public interface ICreateRelation<TPrimary,TForeign,TVal>:ICreateRelation
	{
		new IColumn<TPrimary,TVal> PrimaryColumn
		{
			get;
		}
		new IColumn<TForeign, TVal> ForeignColumn
		{
			get;
		}
	}

}
