using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;
using NetORMLib.Tables;

namespace NetORMLib.Queries
{
	public class Delete<T> : IDelete<T>
	{
		private ITable table;
		public ITable Table => table;

		private List<IFilter<T>> filters;
		IEnumerable<IFilter> IFilterableQuery.Filters => filters;
		public IEnumerable<IFilter<T>> Filters => filters;

		public Delete()
		{
			filters = new List<IFilter<T>>();
		}
		public IDelete<T> From(ITable Table)
		{
			table = Table;
			return this;
		}


		public IDelete<T> Where(params IFilter<T>[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}


	
	}
}
