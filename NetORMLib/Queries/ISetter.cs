using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ISetter
	{
		IColumn Column
		{
			get;
		}
		object Value
		{
			get;
		}
	}
	public interface ISetter<T> : ISetter
	{

	}
	public interface ISetter<T,TVal>:ISetter<T>
	{
		new IColumn<T,TVal> Column
		{
			get;
		}
		new TVal Value
		{
			get;
		}
	}
}
