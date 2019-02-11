using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[module: System.Runtime.CompilerServices.NonNullTypes]

namespace NetORMLibUnitTest.Models
{
	public class Personn
	{
		public static readonly IColumn<Personn, int> PersonnID = new Column<Personn, int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<Personn, string> FirstName = new Column<Personn, string>() {DefaultValue="Homer" };
		public static readonly IColumn<Personn, string> LastName = new Column<Personn, string>() { DefaultValue = "Simpson" };
		public static readonly IColumn<Personn, int?> SecureCode = new Column<Personn, int?>();
		public static readonly IColumn<Personn, string?> Job = new Column<Personn, string?>();
	}
}
