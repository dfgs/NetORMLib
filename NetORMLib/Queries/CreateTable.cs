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
	public class CreateTable : ICreateTable
	{
		private ITable table;
		public ITable Table => table;

		private List<IColumn> columns;
		public IEnumerable<IColumn> Columns => columns;



		public CreateTable(ITable Table, params IColumn[] Columns)
		{
			if (Table==null) throw new ArgumentNullException("Table");
			this.table=Table;
			if ((Columns == null) || (Columns.Length == 0)) throw new ArgumentNullException("Columns");
			columns=new List<IColumn>();
			columns.AddRange(Columns);
		}

	

		

	
	}
}
