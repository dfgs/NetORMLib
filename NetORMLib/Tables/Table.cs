using NetORMLib.Attributes;
using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public class Table : ISingleTable
	{
		private static Regex nameRegex = new Regex("(.+)Table$");

		private string name;
		public string Name => name;

		
		public Table()
		{
			TableAttribute tableAttribute;
			FieldInfo[] fis;
			IColumn column;
			Type type;

			type = GetType();
			tableAttribute = type.GetCustomAttribute<TableAttribute>();

			if (tableAttribute != null)
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

			fis = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach (FieldInfo fi in fis)
			{
				if (!typeof(IColumn).IsAssignableFrom(fi.FieldType)) continue;
				column = fi.GetValue(null) as IColumn;
				column.Initialize(this,fi.Name);
			}
		}


	}
}
