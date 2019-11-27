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
	public class Insert<T> : IInsert<T>
	{
		public string Table => TableDefinition<T>.Name;


		private List<ISetter<T>> setters;
		IEnumerable<ISetter> IInsert.Setters => setters;
		public IEnumerable<ISetter<T>> Setters => setters;

		public Insert()
		{
			setters = new List<ISetter<T>>();
		}

		
		public IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
		where TVal: IDbType
		{
			setters.Add(new Setter<T, TVal>(Column, Value));
			return this;
		}


	}
}
