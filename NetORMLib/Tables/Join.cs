using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public class Join : IJoin
	{
		public ISingleTable OtherTable
		{
			get;
			private set;
		}

		public IColumn FirstColumn
		{
			get;
			private set;
		}


		public IColumn SecondColumn
		{
			get;
			private set;
		}

		public Join(ISingleTable OtherTable, IColumn FirstColumn, IColumn SecondColumn)
		{
			this.OtherTable = OtherTable;this.FirstColumn = FirstColumn;this.SecondColumn = SecondColumn;
		}
			
	}
}
