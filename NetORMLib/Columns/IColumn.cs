using NetORMLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Columns
{
	public interface IColumn
	{
		string Name
		{
			get;
		}
	}

	public interface IColumn<T>:IColumn
	{

	}

	public interface IColumn<T,TVal> : IColumn<T>
	{
		IEqualsFilter<T, TVal> Equals(TVal Value);
	}

}
