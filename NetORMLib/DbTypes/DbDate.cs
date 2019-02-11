using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.DbTypes
{
	public class DbDate:DbType<DateTime>
	{
	

		public DbDate(DateTime Value):base(Value)
		{
		}

		public static implicit operator DbDate(DateTime Value)
		{
			return new DbDate(Value);
		}



	}
}
