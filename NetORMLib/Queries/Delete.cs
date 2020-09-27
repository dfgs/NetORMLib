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
	public class Delete : IDelete
	{
		private ITable table;
		public ITable Table => table;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;

		public Delete()
		{
			filters = new List<IFilter>();
		}
		public IDelete From(ITable Table)
		{
			table = Table;
			return this;
		}


		public IDelete Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}


	
	}
}
