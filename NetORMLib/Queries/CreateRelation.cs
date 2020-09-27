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
	public class CreateRelation<TPrimary,TForeign,TPrimaryVal> : ICreateRelation<TPrimary,TForeign,TPrimaryVal>
	{
		private ITable table;
		public ITable Table => table;

		private IColumn<TPrimary,TPrimaryVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TPrimary,TPrimaryVal> PrimaryColumn => primaryColumn;

		private IColumn<TForeign, TPrimaryVal> foreignColumn;

		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TForeign, TPrimaryVal> ForeignColumn => foreignColumn;


		public CreateRelation(ITable Table, IColumn<TPrimary, TPrimaryVal> PrimaryColumn, IColumn<TForeign, TPrimaryVal> ForeignColumn)
		{
			if (Table == null) throw new ArgumentNullException("Table");
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.table = Table;
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}

		
	}
	public class CreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal> : ICreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal>
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
			this.table = Table;
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}


	}
}
