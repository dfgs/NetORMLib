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
	public class CreateRelation<TVal> : ICreateRelation<TVal>
	{
		private ITable table;
		public ITable Table => table;

		private IColumn<TVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TVal> PrimaryColumn => primaryColumn;

		private IColumn<TVal> foreignColumn;

		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TVal> ForeignColumn => foreignColumn;


		public CreateRelation(ITable Table, IColumn<TVal> PrimaryColumn, IColumn<TVal> ForeignColumn)
		{
			if (Table == null) throw new ArgumentNullException("Table");
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.table=Table;
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}

		
	}
	/*public class CreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal> : ICreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal>
	{
		private ITable table;
		public ITable Table => table;

		private IColumn<TPrimary, TPrimaryVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TPrimary, TPrimaryVal> PrimaryColumn => primaryColumn;

		private IColumn<TForeign, TForeignVal> foreignColumn;

		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TForeign, TForeignVal> ForeignColumn => foreignColumn;


		public CreateRelation(ITable Table, IColumn<TPrimary, TPrimaryVal> PrimaryColumn, IColumn<TForeign, TForeignVal> ForeignColumn)
		{
			if (Table == null) throw new ArgumentNullException("Table");
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.table=Table;
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}


	}*/
}
