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


		static Table()
		{
			Type type;
			FieldInfo[] fis;
			object value;

			columns = new List<IColumn<T>>();
			type = typeof(T);

			fis=type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach(FieldInfo fi in fis)
			{
				if (!fi.FieldType.IsAssignableFrom(typeof(IColumn<T>))) continue;
				value = fi.GetValue(null);
				columns.Add((IColumn<T>)value);

			}
		}

	}
}
