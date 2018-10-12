using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Select : ISelect
	{
		private string table;
		public string Table => table;

		private List<IColumn> columns;
		public IEnumerable<IColumn> Columns => columns;

		private List<IColumn> orders;
		public IEnumerable<IColumn> Orders => orders;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;

	
		public Select(params IColumn[] Columns)
		{
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Must must specify at least one column");
			columns = new List<IColumn>();
			filters = new List<IFilter>();
			orders = new List<IColumn>();

			columns.AddRange(Columns);
		}

		public ISelect From<T>()
		{
			this.table = Table<T>.Name;
			return this;
		}

		IFilterableQuery IFilterableQuery.Where(params IFilter[] Filters)
		{
			return Where(Filters);
		}
		public ISelect Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		IOrderableQuery IOrderableQuery.OrderBy(params IColumn[] Columns)
		{
			return OrderBy(Columns);
		}
		public ISelect OrderBy(params IColumn[] Columns)
		{
			orders.AddRange(Columns);
			return this;
		}

	
	}
}
