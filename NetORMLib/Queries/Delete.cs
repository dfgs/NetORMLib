using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Delete<T> : IDelete<T>
	{
		public string Table => Table<T>.Name;

		private List<IFilter<T>> filters;
		IEnumerable<IFilter> IFilterableQuery.Filters => filters;
		public IEnumerable<IFilter<T>> Filters => filters;

		public Delete()
		{
			filters = new List<IFilter<T>>();
		}

		

		public IDelete<T> Where(params IFilter<T>[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}


	
	}
}
