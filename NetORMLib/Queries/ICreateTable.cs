using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ICreateTable:IQuery
	{
		IEnumerable<IColumn> Columns
		{
			get;
		}
	}

	public interface ICreateTable<T>:ICreateTable
	{
		new IEnumerable<IColumn<T>> Columns
		{
			get;
		}
	}

}
