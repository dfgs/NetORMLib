using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;


namespace NetORMLib.Queries
{
	

	public class ValueSetter<T, TVal> : IValueSetter<T, TVal>
		where TVal:struct
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

		object ISetter.Value
		{
			get { return Value; }
		}
		public TVal? Value
		{
			get;
			set;
		}

		/*public Setter()
		{

		}*/

		public ValueSetter(IColumn<T, TVal> Column,TVal? Value)
		{
			this.Column = Column;this.Value = Value;
		}

	}




}
