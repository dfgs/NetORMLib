using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;

using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class CreateRelation<TPrimary,TForeign,TPrimaryVal> : ICreateRelation<TPrimary,TForeign,TPrimaryVal>
	{
		public string Table => TableDefinition<TForeign>.Name;

		private IColumn<TPrimary,TPrimaryVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TPrimary,TPrimaryVal> PrimaryColumn => primaryColumn;

		private IColumn<TForeign, TPrimaryVal> foreignColumn;

		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TForeign, TPrimaryVal> ForeignColumn => foreignColumn;


		public CreateRelation(IColumn<TPrimary, TPrimaryVal> PrimaryColumn, IColumn<TForeign, TPrimaryVal> ForeignColumn)
		{
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}

		
	}
	public class CreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal> : ICreateRelation<TPrimary, TForeign, TPrimaryVal,TForeignVal>
	{
		public string Table => TableDefinition<TForeign>.Name;

		private IColumn<TPrimary, TPrimaryVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TPrimary, TPrimaryVal> PrimaryColumn => primaryColumn;

		private IColumn<TForeign, TForeignVal> foreignColumn;

		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TForeign, TForeignVal> ForeignColumn => foreignColumn;


		public CreateRelation(IColumn<TPrimary, TPrimaryVal> PrimaryColumn, IColumn<TForeign, TForeignVal> ForeignColumn)
		{
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}


	}
}
