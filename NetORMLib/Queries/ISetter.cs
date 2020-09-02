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
	public interface IValueSetter<T,TVal>:ISetter<T>
		where TVal:struct
	{
		new IColumn<T,TVal> Column
		{
			get;
		}
		
		new TVal? Value
		{
			get;
		}
	}
	public interface IClassSetter<T, TVal> : ISetter<T>
		where TVal : class
	{
		new IColumn<T, TVal> Column
		{
			get;
		}

		new TVal Value
		{
			get;
		}
	}
}
