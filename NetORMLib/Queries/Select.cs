using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Select<T> : ISelect<T>
	{
		public string Table => TableDefinition<T>.Name;

		private List<IColumn<T>> columns;
		IEnumerable<IColumn> ISelect.Columns=> columns;
		public IEnumerable<IColumn<T>> Columns => columns;

		private List<IColumn<T>> orders;
		IEnumerable<IColumn> IOrderableQuery.Orders => orders;
		public IEnumerable<IColumn<T>> Orders => orders;

		private List<IFilter<T>> filters;
		IEnumerable<IFilter> IFilterableQuery.Filters => filters;
		public IEnumerable<IFilter<T>> Filters => filters;


		public Select(params IColumn<T>[] Columns)
		{
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Columns");
			columns = new List<IColumn<T>>();
			filters = new List<IFilter<T>>();
			orders = new List<IColumn<T>>();

			columns.AddRange(Columns);
		}



		public ISelect<T> Where(params IFilter<T>[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		
		public ISelect<T> OrderBy(params IColumn<T>[] Columns)
		{
			orders.AddRange(Columns);
			return this;
		}

	
	}
}
