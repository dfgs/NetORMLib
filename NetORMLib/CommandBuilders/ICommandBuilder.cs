using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.CommandBuilders
{
	public interface ICommandBuilder
	{
		DbCommand BuildCommand(IQuery Query);
	}
}
