using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;

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

		
		public IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal? Value)
			where TVal:struct
		{
			setters.Add(new ValueSetter<T, TVal>(Column, Value));
			return this;
		}

		public IInsert<T> Set<TVal>(IColumn<T, TVal> Column, TVal Value)
			where TVal : class
		{
			setters.Add(new ClassSetter<T, TVal>(Column, Value));
			return this;
		}

	}
}
