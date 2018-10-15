using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Update : IUpdate
	{
		private string table;
		public string Table => table;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;

		private List<ISetter> setters;
		public IEnumerable<ISetter> Setters => setters;

		public Update()
		{
			filters = new List<IFilter>();
			setters = new List<ISetter>();
		}


		IFilterableQuery IFilterableQuery.Where(params IFilter[] Filters)
		{
			return Where(Filters);
		}
		public IUpdate Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		public IUpdate Set<T, TVal>(IColumn<T, TVal> Column, TVal Value)
		{
			this.table = Table<T>.Name;
			setters.Add(new Setter<T,TVal>(Column, Value));
			return this;
		}

		public IUpdate Set<T, TVal>(IEnumerable<ISetter<T, TVal>> Setters)
		{
			setters.AddRange(Setters);
			return this;
		}



	}
}
