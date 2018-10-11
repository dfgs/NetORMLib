using NetORMLib.CommandBuilders;
using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Sql
{
	public class SqlCommandBuilder : ICommandBuilder
	{

		public DbCommand BuildCommand(IQuery Query)
		{
			throw new NotImplementedException();
		}
	}
}
