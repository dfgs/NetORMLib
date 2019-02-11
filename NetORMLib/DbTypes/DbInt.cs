using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.DbTypes
{
	public class DbInt:DbType<int>
	{
		
		public DbInt(int Value):base(Value)
		{
		}

		public static implicit operator DbInt(int Value)
		{
			return new DbInt(Value);
		}

	}
}
