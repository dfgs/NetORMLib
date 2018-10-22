using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class CreateTable<T> : ICreateTable<T>
	{
		public string Table => Table<T>.Name;

		private List<IColumn<T>> columns;
		IEnumerable<IColumn> ICreateTable.Columns => columns;
		public IEnumerable<IColumn<T>> Columns => columns;



		public CreateTable(params IColumn<T>[] Columns)
		{
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Must must specify at least one column");
			columns = new List<IColumn<T>>();
			columns.AddRange(Columns);
		}

	

		

	
	}
}
