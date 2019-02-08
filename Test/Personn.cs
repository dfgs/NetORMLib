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
		public static readonly IColumn<Personn, string> FirstName = new Column<Personn, string>();
		public string firstName
		{
			get ;
		}
		public static readonly IColumn<Personn, string> LastName = new Column<Personn, string>();

	}
}
