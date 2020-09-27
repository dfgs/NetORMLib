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
	public class Update<T> : IUpdate<T>
	{
		private ITable table;
		public ITable Table => table;

		private List<IFilter<T>> filters;
		IEnumerable<IFilter> IFilterableQuery.Filters => filters;
		public IEnumerable<IFilter<T>> Filters => filters;

		private List<ISetter<T>> setters;
		IEnumerable<ISetter> IUpdate.Setters => setters;
		public IEnumerable<ISetter<T>> Setters => setters;

		public Update(ITable Table)
		{
			this.table = Table;
			filters = new List<IFilter<T>>();
			setters = new List<ISetter<T>>();
			
		}


		public IUpdate<T> Where(params IFilter<T>[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		
		public IUpdate<T> Set<TVal>(IColumn<T, TVal> Column, TVal? Value)
			where TVal:struct
		{
			setters.Add(new ValueSetter<T,TVal>(Column, Value));
			return this;
		}
		public IUpdate<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal : class
		{
			setters.Add(new ClassSetter<T, TVal>(Column, Value));
			return this;
		}



	}
}
