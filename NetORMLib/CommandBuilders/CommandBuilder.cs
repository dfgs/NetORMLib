using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;
using NetORMLib.Queries;
using NetORMLib.Tables;

namespace NetORMLib.CommandBuilders
{
	public abstract class CommandBuilder : ICommandBuilder
	{
		protected abstract string OnFormatTableName(ITable Table);
		protected abstract string OnFormatColumnName(IColumn Column,bool FullName);
		protected abstract string OnFormatParameterName(string Column,ref int Index);
		protected abstract string OnFormatFilter(IFilter Filter, ref int Index);
		protected abstract string OnFormatSetter(ISetter Setter, ref int Index);

		protected abstract DbCommand OnBuildSelectCommand(ISelect Query);
		protected abstract DbCommand OnBuildDeleteCommand(IDelete Query);
		protected abstract DbCommand OnBuildUpdateCommand(IUpdate Query);
		protected abstract DbCommand OnBuildInsertCommand(IInsert Query);
		protected abstract DbCommand OnBuildSelectIdentityCommand(ISelectIdentity Query);
		protected abstract DbCommand OnBuildCreateTableCommand(ICreateTable Query);
		protected abstract DbCommand OnBuildCreateRelationCommand(ICreateRelation Query);
		protected abstract DbCommand OnBuildCreateConstraintCommand(ICreateConstraint Query);
		protected abstract DbCommand OnBuildCreateColumnCommand(ICreateColumn Query);


		public DbCommand BuildCommand(IQuery Query)
		{
			if (Query == null) throw new ArgumentNullException("Query");

			if (Query is ISelectIdentity selectIdentity) return OnBuildSelectIdentityCommand(selectIdentity);
			if (Query.Table == null) throw new InvalidOperationException("No table specified in query");
			if (Query is ISelect select) return OnBuildSelectCommand(select);
			if (Query is IDelete delete) return OnBuildDeleteCommand(delete);
			if (Query is IUpdate update) return OnBuildUpdateCommand(update);
			if (Query is IInsert insert) return OnBuildInsertCommand(insert);
			if (Query is ICreateTable createTable) return OnBuildCreateTableCommand(createTable);
			if (Query is ICreateRelation createRelation) return OnBuildCreateRelationCommand(createRelation);
			if (Query is ICreateColumn createColumn) return OnBuildCreateColumnCommand(createColumn);
			if (Query is ICreateConstraint createConstraint) return OnBuildCreateConstraintCommand(createConstraint);
			else throw new NotSupportedException($"Query of type {Query.GetType().Name} is not supported");
		}



	}
}
