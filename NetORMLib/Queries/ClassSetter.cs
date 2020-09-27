using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;


namespace NetORMLib.Queries
{
	

	public class ClassSetter<TVal> : IClassSetter<TVal>
		where TVal:class
	{
		IColumn ISetter.Column
		{
			get { return Column; }
		}
		public IColumn<TVal> Column
		{
			get;
			set;
		}

		object ISetter.Value
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

		public ClassSetter(IColumn<TVal> Column,TVal Value)
		{
			this.Column = Column;this.Value = Value;
		}

	}




}
