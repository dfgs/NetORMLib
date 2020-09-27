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
	public class Insert : IInsert
	{
		private ITable table;
		public ITable Table => table;


		private List<ISetter> setters;
		public IEnumerable<ISetter> Setters => setters;

		public Insert()
		{
			setters = new List<ISetter>();
		}

		public IInsert Into(ITable Table)
		{
			table = Table;
			return this;
		}
		public IInsert Set<TVal>(IColumn<TVal> Column, TVal? Value)
			where TVal:struct
		{
			setters.Add(new ValueSetter<TVal>(Column, Value));
			return this;
		}

		public IInsert Set<TVal>(IColumn<TVal> Column, TVal Value)
			where TVal : class
		{
			setters.Add(new ClassSetter<TVal>(Column, Value));
			return this;
		}

	}
}
