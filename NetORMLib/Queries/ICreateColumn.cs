using NetORMLib.Columns;
using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ICreateColumn:IQuery
	{
		IColumn Column
		{
			get;
		}
	}

	public interface ICreateColumn<T>:ICreateColumn
	{
		new IColumn<T> Column
		{
			get;
		}
	}

}
