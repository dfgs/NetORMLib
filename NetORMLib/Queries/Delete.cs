using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Delete : IDelete
	{
		private string table;
		public string Table => table;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;

		public Delete()
		{
			filters = new List<IFilter>();

		}

		public IDelete From<T>()
		{
			this.table = Table<T>.Name;
			return this;
		}

		IFilterableQuery IFilterableQuery.Where(params IFilter[] Filters)
		{
			return Where(Filters);
		}
		public IDelete Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}


	
	}
}
