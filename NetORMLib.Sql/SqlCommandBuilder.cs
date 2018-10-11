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
		protected override string OnFormatColumnName(string Column)
		{
			return $"[{Column}]";
		}
		protected override string OnFormatParameterName(string Column, int Index)
		{
			return $"@{Column}{Index}";
		}
		protected override string OnFormatFilter(IFilter Filter, int Index)
		{
			
			if (Filter is IColumnFilter columnFilter) return columnFilter.Format(OnFormatColumnName(columnFilter.Column.Name), OnFormatParameterName(columnFilter.Column.Name, Index));
			if (Filter is IIsNullFilter nullFilter) return nullFilter.Format(OnFormatColumnName(nullFilter.Column.Name));
			if (Filter is IIsNotNullFilter notNullFilter) return notNullFilter.Format(OnFormatColumnName(notNullFilter.Column.Name));

			throw new NotImplementedException($"Filter type {Filter.GetType().Name} is not implemented");
		}


		protected override DbCommand OnBuildSelectCommand(ISelect Query)
		{
			SqlCommand command;
			StringBuilder sql;
			int index;

			sql = new StringBuilder();
			sql.Append("SELECT ");
			sql.Append(String.Join(", ", Query.Columns.Select(item => OnFormatColumnName(item.Name))));
			sql.Append(" FROM ");
			sql.Append(OnFormatTableName(Query.Table));
			
			if (Query.Filters.Any())
			{
				index = 0;
				sql.Append(" WHERE ");
				sql.Append(String.Join(" AND ", Query.Filters.Select(item => OnFormatFilter(item,index++)  )));
			}

			command = new SqlCommand(sql.ToString());

			foreach (IFilter filter in Query.Filters)
			{
				index = 0;
				if (filter is IColumnFilter columnFilter)
				{
					command.Parameters.AddWithValue(OnFormatParameterName(columnFilter.Column.Name, index), columnFilter.Value );
				}
				index++;
			}

			return command;
		}

		

	}
}
