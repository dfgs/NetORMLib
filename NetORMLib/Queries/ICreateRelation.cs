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

	public interface ICreateRelation<TVal>:ICreateRelation
	{
		new IColumn<TVal> PrimaryColumn
		{
			get;
		}
		new IColumn<TVal> ForeignColumn
		{
			get;
		}
	}
	
}
