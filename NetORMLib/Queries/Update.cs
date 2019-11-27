using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.DbTypes;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Update<T> : IUpdate<T>
	{
		public string Table => TableDefinition<T>.Name;

		private List<IFilter<T>> filters;
		IEnumerable<IFilter> IFilterableQuery.Filters => filters;
		public IEnumerable<IFilter<T>> Filters => filters;

		private List<ISetter<T>> setters;
		IEnumerable<ISetter> IUpdate.Setters => setters;
		public IEnumerable<ISetter<T>> Setters => setters;

		public Update()
		{
			filters = new List<IFilter<T>>();
			setters = new List<ISetter<T>>();
			
		}


		public IUpdate<T> Where(params IFilter<T>[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		
		public IUpdate<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal: IDbType
		{
			setters.Add(new Setter<T,TVal>(Column, Value));
			return this;
		}
		



	}
}
