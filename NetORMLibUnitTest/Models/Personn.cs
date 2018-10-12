using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Models
{
	public class Personn
	{
		public static readonly IColumn<Personn, string> FirstName = new Column<Personn, string>() {DefaultValue="Homer" };
		public static readonly IColumn<Personn, string> LastName = new Column<Personn, string>() { DefaultValue = "Simpson" };
	}
}
