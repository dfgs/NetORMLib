using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib
{
	public interface IRow//: System.Dynamic.IDynamicMetaObjectProvider
	{
		object GetValue(IColumn Column);
		void SetValue(IColumn Column, object Value);

	}
	public interface IRow<T>: IRow
	{
		TVal GetValue<TVal>(IColumn<T, TVal> Column);
		void SetValue<TVal>(IColumn<T, TVal> Column, TVal Value);



	}
}
