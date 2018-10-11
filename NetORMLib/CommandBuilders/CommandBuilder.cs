using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Filters;
using NetORMLib.Queries;

namespace NetORMLib.CommandBuilders
{
	public abstract class CommandBuilder : ICommandBuilder
	{
		protected abstract string OnFormatTableName(string Table);
		protected abstract string OnFormatColumnName(string Column);
		protected abstract string OnFormatParameterName(string Column, int Index);
		protected abstract string OnFormatFilter(IFilter Filter,int Index);

		protected abstract DbCommand OnBuildSelectCommand(ISelect Query);
		

		public DbCommand BuildCommand(IQuery Query)
		{
			if (Query == null) throw new ArgumentNullException("Query");

			if (Query is ISelect) return OnBuildSelectCommand((ISelect)Query);
			else throw new NotSupportedException($"Query of type {Query.GetType().Name} is not supported");
		}
	}
}
