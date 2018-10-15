using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;
using NetORMLib.Queries;

namespace NetORMLib.CommandBuilders
{
	public abstract class CommandBuilder : ICommandBuilder
	{
		protected abstract string OnFormatTableName(string Table);
		protected abstract string OnFormatColumnName(IColumn Column);
		protected abstract string OnFormatParameterName(string Column,ref int Index);
		protected abstract string OnFormatFilter(IFilter Filter, ref int Index);
		protected abstract string OnFormatSetter(ISetter Setter, ref int Index);

		protected abstract DbCommand OnBuildSelectCommand(ISelect Query);
		protected abstract DbCommand OnBuildDeleteCommand(IDelete Query);
		protected abstract DbCommand OnBuildUpdateCommand(IUpdate Query);


		public DbCommand BuildCommand(IQuery Query)
		{
			if (Query == null) throw new ArgumentNullException("Query");
			if (Query.Table == null) throw new InvalidOperationException("No table specified in query");

			if (Query is ISelect select) return OnBuildSelectCommand(select);
			if (Query is IDelete delete) return OnBuildDeleteCommand(delete);
			if (Query is IUpdate update) return OnBuildUpdateCommand(update);
			else throw new NotSupportedException($"Query of type {Query.GetType().Name} is not supported");
		}
	}
}
