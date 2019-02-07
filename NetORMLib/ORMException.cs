using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib
{
	[Serializable]
	public class ORMException:Exception
	{

		public ORMException(DbCommand Command,Exception InnerException):base($"Failed to run query: {Command.CommandText}",InnerException)
		{
			
		}
	}
}
