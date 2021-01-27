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
	public class CreateConstraint : ICreateConstraint
	{
		private ITable table;
		public ITable Table => table;

		public ColumnConstraints Constraint
		{
			get;
		}

		private IColumn[] columns;
		public IEnumerable<IColumn> Columns
		{ 
			get => columns; 
		}

		
		


		public CreateConstraint(ITable Table, ColumnConstraints Constraint,params IColumn[] Columns)
		{
			if (Table == null) throw new ArgumentNullException("Table");
			if (Columns == null) throw new ArgumentNullException("Columns");
			this.table=Table;
			this.Constraint = Constraint;this.columns = Columns;
		}

	}


}
