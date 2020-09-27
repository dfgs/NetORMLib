using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;


namespace NetORMLib.Queries
{
	

	public class ValueSetter<TVal> : IValueSetter< TVal>
		where TVal:struct
	{
		IColumn ISetter.Column
		{
			get { return Column; }
		}
		public IColumn< TVal> Column
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

		public ValueSetter(IColumn<TVal> Column,TVal? Value)
		{
			this.Column = Column;this.Value = Value;
		}

	}




}
