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
	public class CreateTable<T> : ICreateTable<T>
	{
		private ITable table;
		public ITable Table => table;

		private List<IColumn<T>> columns;
		IEnumerable<IColumn> ICreateTable.Columns => columns;
		public IEnumerable<IColumn<T>> Columns => columns;



		public CreateTable(ITable Table, params IColumn<T>[] Columns)
		{
			if (Table==null) throw new ArgumentNullException("Table");
			this.table = Table;
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Columns");
			columns = new List<IColumn<T>>();
			columns.AddRange(Columns);
		}

	

		

	
	}
}
