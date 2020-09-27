using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface IOrderableQuery : IQuery
	{
		OrderModes OrderMode
		{
			get;
		}

		IEnumerable<IColumn> Orders
		{
			get;
		}

	}

	

}
