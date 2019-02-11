using NetORMLib.Columns;
using NetORMLib.DbTypes;
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
		IDbType Value
		{
			get;
		}
	}
	public interface ISetter<T> : ISetter
	{

	}
	public interface ISetter<T,TVal>:ISetter<T>
		where TVal: IDbType
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
