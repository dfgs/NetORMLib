using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.DbTypes;

namespace NetORMLib.Queries
{
	public class Setter<T, TVal> : ISetter<T, TVal>
		where TVal: IDbType
	{
		IColumn ISetter.Column
		{
			get { return Column; }
		}
		public IColumn<T, TVal> Column
		{
			get;
			set;
		}

		IDbType ISetter.Value
		{
			get { return Value; }
		}
		public TVal Value
		{
			get;
			set;
		}

		/*public Setter()
		{

		}*/

		public Setter(IColumn<T, TVal> Column,TVal Value)
		{
			this.Column = Column;this.Value = Value;
		}

	}
}
