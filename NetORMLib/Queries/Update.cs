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
	public class Update : IUpdate
	{
		private ITable table;
		public ITable Table => table;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;

		private List<ISetter> setters;
		public IEnumerable<ISetter> Setters => setters;

		public Update(ITable Table)
		{
			this.table = Table;
			filters = new List<IFilter>();
			setters = new List<ISetter>();
			
		}


		public IUpdate Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}

		
		public IUpdate Set<TVal>(IColumn<TVal> Column, TVal? Value)
			where TVal:struct
		{
			setters.Add(new ValueSetter<TVal>(Column, Value));
			return this;
		}
		public IUpdate Set<TVal>(IColumn<TVal> Column, TVal Value)
			where TVal : class
		{
			setters.Add(new ClassSetter<TVal>(Column, Value));
			return this;
		}



	}
}
