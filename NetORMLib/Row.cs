using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib
{
	public class Row:DynamicObject
	{
		private Dictionary<string,object> values;

		
		public Row(IEnumerable<IColumn> Columns)
		{
			values = new Dictionary<string, object>();
			foreach (IColumn column in Columns)
			{
				values.Add(column.Name, column.DefaultValue);
			}
		}
		public override bool TryGetMember(GetMemberBinder Binder, out object Result)
		{
			return TryGetMember(Binder.Name, out Result);
		}
		public bool TryGetMember(string Name, out object Result)
		{
			return values.TryGetValue(Name, out Result);
		}
		public bool TryGetMember(IColumn Column, out object Result)
		{
			return TryGetMember(Column.Name, out Result);
		}


		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			return TrySetMember(binder.Name, value);
		}
		public bool TrySetMember(string Name, object Value)
		{
			if (!values.ContainsKey(Name)) return false;
			values[Name] = Value;
			return true;
		}
		public bool TrySetMember(IColumn Column,object Value)
		{
			return TrySetMember(Column.Name, Value);
		}



	}
}
