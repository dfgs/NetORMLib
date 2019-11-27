using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class CreateColumn<T> : ICreateColumn<T>
	{
		public string Table => TableDefinition<T>.Name;

		private IColumn<T> column;
		IColumn ICreateColumn.Column => column;
		public IColumn<T> Column => column;


		public CreateColumn(IColumn<T> Column)
		{
			if (Column == null) throw new ArgumentNullException("Column");
			this.column = Column;
		}

	

		

	
	}
}
