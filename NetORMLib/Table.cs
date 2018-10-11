using NetORMLib.Attributes;
using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib
{
	public static class Table<T>
	{
		private static List<IColumn<T>> columns;
		public static IEnumerable<IColumn<T>> Columns
		{
			get { return columns; }
		}

		private static string name;
		public static string Name
		{
			get { return name; }
		}

		static Table()
		{
			Type type;
			FieldInfo[] fis;
			object value;
			TableAttribute tableAttribute;

			columns = new List<IColumn<T>>();
			type = typeof(T);
			tableAttribute = type.GetCustomAttribute<TableAttribute>();

			
			name = tableAttribute==null?type.Name: tableAttribute.Name;

			fis=type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach(FieldInfo fi in fis)
			{
				if (!typeof(IColumn<T>).IsAssignableFrom(fi.FieldType)) continue;
				value = fi.GetValue(null);
				columns.Add((IColumn<T>)value);

			}
		}


	}
}
