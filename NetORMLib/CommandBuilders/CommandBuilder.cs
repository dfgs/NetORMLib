using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Queries;

namespace NetORMLib.CommandBuilders
{
	public abstract class CommandBuilder : ICommandBuilder
	{
		protected abstract DbCommand OnBuildSelectCommand(ISelect Query);
		

		public DbCommand BuildCommand(IQuery Query)
		{
			throw new NotImplementedException();
		}
	}
}
