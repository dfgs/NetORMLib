using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class Personn
	{
		public static readonly IColumn<Personn, string> FirstNameColumn = new Column<Personn, string>();
		public static readonly IColumn<Personn, string> LastNameColumn = new Column<Personn, string>();

	}
}
