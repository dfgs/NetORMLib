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
	
	public interface IValueSetter<TVal>:ISetter
		where TVal:struct
	{
		new IColumn<TVal> Column
		{
			get;
		}
		
		new TVal? Value
		{
			get;
		}
	}
	public interface IClassSetter<TVal> : ISetter
		where TVal : class
	{
		new IColumn<TVal> Column
		{
			get;
		}

		new TVal Value
		{
			get;
		}
	}
}
