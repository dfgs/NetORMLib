using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib
{
	public class Row<T>: IRow<T>//DynamicObject,
	{

		object IRow.GetValue(IColumn Column)
		{
			return Column.GetValue(this);
		}
		void IRow.SetValue(IColumn Column, object Value)
		{
			Column.SetValue(this, Value);
		}

		public TVal GetValue<TVal>(IColumn<T,TVal> Column)
			where TVal:IDbType
		{
			return Column.GetValue(this);
		}
		public void SetValue<TVal>(IColumn<T, TVal> Column,TVal Value)
			where TVal : IDbType
		{
			Column.SetValue(this, Value);
		}
		/*public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			IColumn<T> column;

			try
			{
				column = TableDefinition<T>.GetColumn(binder.Name);
			}
			catch
			{
				result = null;
				return false;
			}
			result = column.GetValue(this);
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			IColumn<T> column;

			try
			{
				column = TableDefinition<T>.GetColumn(binder.Name);
			}
			catch
			{
				return false;
			}
			column.SetValue(this,value);
			return true;
		}*/


	}
}
