using NetORMLib.Columns;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql
{
	public class SqlCommandBuilder : CommandBuilder
	{
		protected override string OnFormatTableName(string Table)
		{
			return $"[{Table}]";
		}
		protected override string OnFormatColumnName(IColumn Column)
		{
			return $"[{Column.Table}].[{Column.Name}]";
		}
		protected override string OnFormatParameterName(string Column,ref int Index)
		{
			return $"@{Column}{Index++}";
		}
		protected override string OnFormatFilter(IFilter Filter,ref int Index)
		{
			
			if (Filter is IColumnFilter columnFilter) return columnFilter.Format(OnFormatColumnName(columnFilter.Column), OnFormatParameterName(columnFilter.Column.Name,ref Index));
			if (Filter is IIsNullFilter nullFilter) return nullFilter.Format(OnFormatColumnName(nullFilter.Column));
			if (Filter is IIsNotNullFilter notNullFilter) return notNullFilter.Format(OnFormatColumnName(notNullFilter.Column));
			if (Filter is IBooleanFilter booleanFilter)
			{
				List<string> formattedMembers = new List<string>();
				foreach (IFilter filter in booleanFilter.Members) formattedMembers.Add(OnFormatFilter(filter, ref Index));
				return booleanFilter.Format(formattedMembers);
			}

			throw new NotImplementedException($"Filter type {Filter.GetType().Name} is not implemented");
		}

		private void OnBuildParameters(SqlCommand Command,IEnumerable<IFilter> Filters,ref int Index)
		{
			foreach (IFilter filter in Filters)
			{
				if (filter is IColumnFilter columnFilter)
				{
					Command.Parameters.AddWithValue(OnFormatParameterName(columnFilter.Column.Name, ref Index), columnFilter.Value);
				}
				else if (filter is IBooleanFilter booleanFilter) OnBuildParameters(Command, booleanFilter.Members, ref Index);
			}
		}

		protected override DbCommand OnBuildSelectCommand(ISelect Query)
		{
			SqlCommand command;
			StringBuilder sql;
			int index;

			sql = new StringBuilder();
			sql.Append("SELECT ");
			sql.Append(String.Join(", ", Query.Columns.Select(item => OnFormatColumnName(item))));
			sql.Append(" FROM ");
			sql.Append(OnFormatTableName(Query.Table));
			
			if (Query.Filters.Any())
			{
				index = 0;
				sql.Append(" WHERE ");
				sql.Append(String.Join(" AND ", Query.Filters.Select(item => OnFormatFilter(item,ref index)  )));
			}

			command = new SqlCommand(sql.ToString());
			index = 0;
			OnBuildParameters(command, Query.Filters,ref index);
			

			return command;
		}

		

	}
}
