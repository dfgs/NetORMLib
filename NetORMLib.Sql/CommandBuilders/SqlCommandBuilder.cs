﻿using NetORMLib.Columns;
using NetORMLib.CommandBuilders;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Tables;
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
		protected override string OnFormatTableName(ITable Table)
		{
			switch(Table)
			{
				case ISingleTable singleTable: return $"[{singleTable.Name}]";
				case IJoinedTables joinedTables: 
					return $"[{joinedTables.FirstTable.Name}]"+ string.Join("", joinedTables.Joins.Select(item=>$" INNER JOIN {OnFormatTableName(item.OtherTable)} ON {OnFormatColumnName(item.FirstColumn)} = {OnFormatColumnName(item.SecondColumn)}" ));
				default: throw (new NotImplementedException("Invalid table type"));
			}
		}

		protected override string OnFormatColumnName(IColumn Column,bool FullName=true)
		{
			return FullName?$"[{Column.Table.Name}].[{Column.Name}]": $"[{Column.Name}]";
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
				Command.Parameters.AddWithValue(OnFormatParameterName(setter.Column.Name, ref Index), setter.Value??DBNull.Value);
			}
		}

		private string GetTypeName(IColumn Column)
		{
			string result;
			Type type, underlyingType;

			

			underlyingType = Nullable.GetUnderlyingType(Column.DataType);
			if (underlyingType != null) type = underlyingType;
			else type = Column.DataType;

			if (type.IsEnum) return "int";

			switch (type.Name)
			{
				case "String":
					result="nvarchar(MAX)";
					break;
				case "Byte":
					result="tinyint";
					break;
				case "Int32":
					result="int";
					break;
				case "UInt32":
					result="int";
					break;
				case "Int64":
					result="bigint";
					break;
				case "Boolean":
					result="bit";
					break;
				case "DateTime":
					result="DateTime";
					break;
				case "TimeSpan":
					result="Time(7)";
					break;
				case "Byte[]":
					result="varbinary(max)";
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
			if (Query.Limit>=0) sql.Append($"TOP({Query.Limit}) ");
			sql.Append(String.Join(", ", Query.Columns.Select(item => OnFormatColumnName(item))));
			sql.Append(" FROM ");
			sql.Append(OnFormatTableName(Query.Table));

			if (Query.Filters.Any())
			{
				index = 0;
				sql.Append(" WHERE ");
				sql.Append(String.Join(" AND ", Query.Filters.Select(item => OnFormatFilter(item, ref index))));
			}
			if (Query.Orders.Any())
			{
				index = 0;
				sql.Append(" ORDER BY ");
				sql.Append(String.Join(", ", Query.Orders.Select(item => OnFormatColumnName(item))));
				if (Query.OrderMode==OrderModes.ASC) sql.Append(" ASC"); else sql.Append(" DESC");
			}


			command=new SqlCommand(sql.ToString());
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

			sql=new StringBuilder();
			sql.Append("UPDATE ");
			sql.Append(OnFormatTableName(Query.Table));

			sql.Append(" SET ");
			index=0;
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

				index=0;
				sql.Append(String.Join(", ", Query.Setters.Select(item => OnFormatParameterName(item.Column.Name, ref index))));
				sql.Append(")");
			}

			command=new SqlCommand(sql.ToString());
			index = 0;
			OnBuildParameters(command, Query.Setters, ref index);
			
			return command;
		}

		protected override DbCommand OnBuildSelectIdentityCommand(ISelectIdentity Query)
		{
			return new SqlCommand("SELECT @@IDENTITY");
			//return new SqlCommand("SELECT SCOPE_IDENTITY()");
			
		}

		private string GetConstraint(IColumn Column)
		{
			switch(Column.Constraint)
			{
				case ColumnConstraints.None:return "";
				case ColumnConstraints.PrimaryKey: return " PRIMARY KEY";
				case ColumnConstraints.Unique: return " UNIQUE";
				default:throw (new NotImplementedException("Invalid constraint"));
			}
		}
		
		protected override DbCommand OnBuildCreateTableCommand(ICreateTable Query)
		{
			SqlCommand command;
			StringBuilder sql;

			

			sql = new StringBuilder();
			sql.Append("CREATE TABLE ");
			sql.Append(OnFormatTableName(Query.Table));

			sql.Append(" (");
			sql.Append(String.Join(", ", Query.Columns.Select( item=>  $"{OnFormatColumnName(item,false)} {GetTypeName(item)} {(item.IsNullable ? "NULL" : "NOT NULL")}{GetConstraint(item)}" )   ) );
			sql.Append(")");

			command=new SqlCommand(sql.ToString());
			

			return command;
		}


		protected override DbCommand OnBuildCreateRelationCommand(ICreateRelation Query)
		{
			SqlCommand command;
			StringBuilder sql;

			sql = new StringBuilder();
			sql.Append("ALTER TABLE ");
			sql.Append(OnFormatTableName(Query.Table));
			sql.Append($" WITH CHECK ADD CONSTRAINT [FK_{Query.ForeignColumn.Table.Name}_{Query.ForeignColumn.Name}_{Query.PrimaryColumn.Table.Name}]");
			sql.Append($" FOREIGN KEY ({OnFormatColumnName(Query.ForeignColumn, false)})");
			sql.Append($" REFERENCES [{Query.PrimaryColumn.Table.Name}] ({OnFormatColumnName(Query.PrimaryColumn, false)})");


			command=new SqlCommand(sql.ToString());

			return command;
		}
		protected override DbCommand OnBuildCreateConstraintCommand(ICreateConstraint Query)
		{
			SqlCommand command;
			StringBuilder sql;

			sql = new StringBuilder();
			sql.Append("ALTER TABLE ");
			sql.Append(OnFormatTableName(Query.Table));
			sql.Append($" ADD CONSTRAINT [AK_{Query.Table}_{string.Join("_",Query.Columns.Select(item=>item.Name))}]");
			
			switch(Query.Constraint)
			{
				case ColumnConstraints.PrimaryKey:
					sql.Append($" PRIMARY KEY ({string.Join(", ", Query.Columns.Select(item => OnFormatColumnName(item, false)))})");
					break;
				case ColumnConstraints.Unique:
					sql.Append($" UNIQUE ({string.Join(", ", Query.Columns.Select(item => OnFormatColumnName(item,false)))})");
					break;
				default: throw new NotImplementedException("Invalid constraint");
			}

			command=new SqlCommand(sql.ToString());

			return command;
		}

		protected override DbCommand OnBuildCreateColumnCommand(ICreateColumn Query)
		{
			SqlCommand command;
			StringBuilder sql;


			sql = new StringBuilder();
			sql.Append("ALTER TABLE ");
			sql.Append(OnFormatTableName(Query.Table));
			sql.Append($" ADD {OnFormatColumnName(Query.Column, false)} {GetTypeName(Query.Column)} {(Query.Column.IsNullable ? "NULL" : "NOT NULL")}{GetConstraint(Query.Column)}");

			command=new SqlCommand(sql.ToString());
			return command;

		
		}


	}
}
