using NetORMLib;
using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class Personn
	{
		public static readonly IColumn<Personn, DbString> FirstName = new Column<Personn, DbString>();
		
		public static readonly IColumn<Personn, DbString> LastName = new Column<Personn, DbString>();

	}
}
