using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class CreateTable<T> : ICreateTable
	{
		private string table;
		public string Table => table;

		private List<IColumn> columns;
		public IEnumerable<IColumn> Columns => columns;


	
		public CreateTable(params IColumn[] Columns)
		{
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Must must specify at least one column");
			columns = new List<IColumn>();

			columns.AddRange(Columns);
			this.table = Table<T>.Name;
		}

	

		

	
	}
}
