using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class CreateRelation<TPrimary,TForeign,TVal> : ICreateRelation<TPrimary,TForeign,TVal>
	{
		public string Table => Table<TForeign>.Name;

		private IColumn<TPrimary,TVal> primaryColumn;
		IColumn ICreateRelation.PrimaryColumn => primaryColumn;
		public IColumn<TPrimary,TVal> PrimaryColumn => primaryColumn;

		private IColumn<TForeign, TVal> foreignColumn;
		IColumn ICreateRelation.ForeignColumn => foreignColumn;
		public IColumn<TForeign, TVal> ForeignColumn => foreignColumn;


		public CreateRelation(IColumn<TPrimary, TVal> PrimaryColumn, IColumn<TForeign, TVal> ForeignColumn)
		{
			if (PrimaryColumn == null) throw new ArgumentNullException("PrimaryColumn");
			if (ForeignColumn == null) throw new ArgumentNullException("ForeignColumn");
			this.primaryColumn = PrimaryColumn;
			this.foreignColumn = ForeignColumn;
		}

	

		

	
	}
}
