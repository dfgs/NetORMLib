using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.DbTypes
{
	public class DbString:DbType<string>
	{

		
		public DbString(string Value):base(Value)
		{
		}

		public static implicit operator DbString(string Value)
		{
			return new DbString(Value);
		}

	}
}
