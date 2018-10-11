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
		object GetValue(object Row);
	}

	public interface IColumn<T>:IColumn
	{
		object GetValue(T Row);

	}

	public interface IColumn<T,TVal> : IColumn<T>
	{
		new TVal GetValue(T Row);

		IEqualsFilter<T, TVal> Equals(TVal Value);
	}

}
