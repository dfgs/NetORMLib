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
	public class CreateColumn<T> : ICreateColumn<T>
	{
		private ITable table;
		public ITable Table => table;

		private IColumn<T> column;
		IColumn ICreateColumn.Column => column;
		public IColumn<T> Column => column;


		public CreateColumn(ITable Table, IColumn<T> Column)
		{
			if (Column == null) throw new ArgumentNullException("Column");
			this.column = Column;
			if (Table == null) throw new ArgumentNullException("Table");
			this.table = Table;
		}






	}
}
