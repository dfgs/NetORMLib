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

namespace NetORMLib.Sql.CommandBuilders
{
	public class SqlCommandBuilder : CommandBuilder
	{
		protected override string OnFormatTableName(string Table)
		{
			return $"[{Table}]";
		}
		protected override string OnFormatColumnName(IColumn Column,bool FullName=true)
		{
			return FullName?$"[{Column.Table}].[{Column.Name}]": $"[{Column.Name}]";
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
		protected override string OnFormatSetter(ISetter Setter, ref int Index)
		{
			return $"{OnFormatColumnName(Setter.Column)}={OnFormatParameterName(Setter.Column.Name,ref Index)}";
		}

		private void OnBuildParameters(SqlCommand Command, IEnumerable<IFilter> Filters, ref int Index)
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
		private void OnBuildParameters(SqlCommand Command, IEnumerable<ISetter> Setters, ref int Index)
		{
			foreach (ISetter setter in Setters)
			{
				Command.Parameters.AddWithValue(OnFormatParameterName(setter.Column.Name, ref Index), setter.Value);
			}
		}

		private string GetTypeName(IColumn Column)
		{
			string result;

			if (Column.DataType.IsEnum) return "int";

			switch (Column.DataType.Name)
			{
				case "String":
					result = "nvarchar(MAX)";
					break;
				case "Byte":
					result = "tinyint";
					break;
				case "Int32":
					result = "int";
					break;
				case "UInt16":
					result = "int";
					break;
				case "Int64":
					result = "bigint";
					break;
				case "Boolean":
					result = "bit";
					break;
				case "DateTime":
					result = "DateTime";
					break;
				case "TimeSpan":
					result = "Time(7)";
					break;
				case "Byte[]":
					result = "varbinary(max)";
					break;
				default:
					throw (new NotImplementedException("Cannot convert CLR type " + Column.DataType.Name));

			}
			if (Column.IsIdentity) result += " IDENTITY(1, 1)";
			return result;
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

		protected override DbCommand OnBuildDeleteCommand(IDelete Query)
		{
			SqlCommand command;
			StringBuilder sql;
			int index;

			sql = new StringBuilder();
			sql.Append("DELETE FROM ");
			sql.Append(OnFormatTableName(Query.Table));

			if (Query.Filters.Any())
			{
				index = 0;
				sql.Append(" WHERE ");
				sql.Append(String.Join(" AND ", Query.Filters.Select(item => OnFormatFilter(item, ref index))));
			}

			command = new SqlCommand(sql.ToString());
			index = 0;
			OnBuildParameters(command, Query.Filters, ref index);


			return command;
		}

		protected override DbCommand OnBuildUpdateCommand(IUpdate Query)
		{
			SqlCommand command;
			StringBuilder sql;
			int index;

			if (!Query.Setters.Any()) throw (new InvalidOperationException("Update query must specify at least one setter"));

			sql = new StringBuilder();
			sql.Append("UPDATE ");
			sql.Append(OnFormatTableName(Query.Table));

			sql.Append(" SET ");
			index = 0;
			sql.Append(String.Join(", ", Query.Setters.Select(item => OnFormatSetter(item, ref index))));

			if (Query.Filters.Any())
			{
				sql.Append(" WHERE ");
				sql.Append(String.Join(" AND ", Query.Filters.Select(item => OnFormatFilter(item, ref index))));
			}

			command = new SqlCommand(sql.ToString());
			index = 0;
			OnBuildParameters(command, Query.Setters, ref index);
			OnBuildParameters(command, Query.Filters, ref index);


			return command;
		}

		protected override DbCommand OnBuildInsertCommand(IInsert Query)
		{
			SqlCommand command;
			StringBuilder sql;
			int index;

			sql = new StringBuilder();
			sql.Append("INSERT INTO ");
			sql.Append(OnFormatTableName(Query.Table));

			if (Query.Setters.Any())
			{
				sql.Append(" (");
				sql.Append(String.Join(", ", Query.Setters.Select(item => OnFormatColumnName(item.Column))));
				sql.Append(") VALUES (");

				index = 0;
				sql.Append(String.Join(", ", Query.Setters.Select(item => OnFormatParameterName(item.Column.Name, ref index))));
				sql.Append(")");
			}

			command = new SqlCommand(sql.ToString());
			index = 0;
			OnBuildParameters(command, Query.Setters, ref index);
			
			return command;
		}

		protected override DbCommand OnBuildCreateTableCommand(ICreateTable Query)
		{
			SqlCommand command;
			StringBuilder sql;

			sql = new StringBuilder();
			sql.Append("CREATE TABLE ");
			sql.Append(OnFormatTableName(Query.Table));

			sql.Append(" (");
			sql.Append(String.Join(", ", Query.Columns.Select( item=>  $"{OnFormatColumnName(item,false)} {GetTypeName(item)} {(item.IsNullable ? "NULL" : "NOT NULL")}{(item.IsPrimaryKey ? " PRIMARY KEY" : "")}" )   ) );
			sql.Append(")");

			command = new SqlCommand(sql.ToString());

			return command;
		}

		/*
		 * 
		 protected override SqlCommand OnCreateTableCreateCommand(ITable Table)
		{
			string sql;


			sql = "create table [" + Table.Name + "] (";
			foreach (IColumn column in Table.Columns.Where(item => (item.Revision == 0 ) && !item.IsVirtual))
			{
				sql += OnFormatColumnName(column) + " " + GetTypeName(column) + (column.IsNullable ? " NULL," : " NOT NULL,");
			}
			sql += "CONSTRAINT [PK_" + Table.Name + "]" + " PRIMARY KEY CLUSTERED ([" + Table.PrimaryKey.Name + "] ASC))";

			return new SqlCommand(sql);
		}*/


	}
}
