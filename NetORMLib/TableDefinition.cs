using NetORMLib.Attributes;
using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetORMLib
{
	public static class TableDefinition<T>
	{
		private static List<IColumn> columns;
		public static IEnumerable<IColumn> Columns
		{
			get { return columns; }
		}

		

		private static string name;
		public static string Name
		{
			get { return name; }
		}

		private static Regex nameRegex = new Regex("(.+)Table$");

		static TableDefinition()
		{
			Type type;
			FieldInfo[] fis;
			object value;
			TableAttribute tableAttribute;

			columns = new List<IColumn>();
			type = typeof(T);
			tableAttribute = type.GetCustomAttribute<TableAttribute>();

			if (tableAttribute!=null)
			{
				name = tableAttribute.Name; 
			}
			else
			{
				Match match;

				match = nameRegex.Match(type.Name);
				if (match.Success)
				{
					name = match.Groups[1].Value;
				}
				else
				{
					name = type.Name;
				}
			}

			

			fis =type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach(FieldInfo fi in fis)
			{
				if (!typeof(IColumn).IsAssignableFrom(fi.FieldType)) continue;
				value = fi.GetValue(null);
				columns.Add((IColumn)value);
			}
		}
		public static IColumn GetColumn(string Name)
		{
			IColumn result;

			result = columns.First(item => item.Name == Name);
			return result;
		}


	}
}
