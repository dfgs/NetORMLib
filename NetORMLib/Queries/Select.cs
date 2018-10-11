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
		public string Table
		{
			get { return Table<T>.Name; }
		}

		private List<IColumn<T>> columns;
		public IEnumerable<IColumn<T>> Columns => columns;
		IEnumerable<IColumn> ISelect.Columns => columns;

		private List<IColumn<T>> orders;
		public IEnumerable<IColumn<T>> Orders => orders;
		IEnumerable<IColumn> ISelect.Orders => orders;

		private List<IFilter<T>> filters;
		public IEnumerable<IFilter<T>> Filters => filters;
		IEnumerable<IFilter> ISelect.Filters => filters;



	
		public Select(params IColumn<T>[] Columns)
		{
			columns = new List<IColumn<T>>();
			filters = new List<IFilter<T>>();
			orders = new List<IColumn<T>>();

			if ((Columns == null) || (Columns.Length == 0)) columns.AddRange(Table<T>.Columns);
			else columns.AddRange(Columns);
		}

		#region ISelect
		IQuery<T> IFilterableQuery<T>.Where(params IFilter<T>[] Filters)
		{
			return Where(Filters);
		}

		IQuery<T> IOrderableQuery<T>.OrderBy(params IColumn<T>[] Columns)
		{
			return OrderBy(Columns);
		}
		#endregion

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
