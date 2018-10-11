using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IOrderableQuery<T> : IQuery<T>
	{
		IQuery<T> OrderBy(params IColumn<T>[] Columns);
	}
}
