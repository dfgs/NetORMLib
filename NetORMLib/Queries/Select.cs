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
	public class Select : ISelect
	{
		public OrderModes OrderMode
		{
			get;
			private set;
		}

		public int Limit
		{
			get;
			private set;
		}

		private ITable table;
		public ITable Table => table;

		private List<IColumn> columns;
		public IEnumerable<IColumn> Columns => columns;

		private List<IColumn> orders;
		public IEnumerable<IColumn> Orders => orders;

		private List<IFilter> filters;
		public IEnumerable<IFilter> Filters => filters;


		public Select(params IColumn[] Columns)
		{
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Columns");
			Limit = -1;
			columns = new List<IColumn>();
			filters = new List<IFilter>();
			orders = new List<IColumn>();

			columns.AddRange(Columns);
		}

	

		public ISelect From(ITable Table)
		{
			table=Table;
			return this;
		}

		public ISelect Where(params IFilter[] Filters)
		{
			filters.AddRange(Filters);
			return this;
		}


		public ISelect OrderBy(params IColumn[] Columns)
		{
			return OrderBy(OrderModes.ASC, Columns);
		}
		public ISelect OrderBy(OrderModes OrderMode,params IColumn[] Columns)
		{
			this.OrderMode = OrderMode;
			orders.AddRange(Columns);
			return this;
		}

		public ISelect Top(int Limit)
		{
			this.Limit = Limit;
			return this;
		}

		

	}
}
