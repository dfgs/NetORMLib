using NetORMLib;
using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace NetORMLibUnitTest.Models
{
	public class Personn
	{
		public static readonly IColumn<Personn, int> PersonnID = new Column<Personn, int>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<Personn, string> FirstName = new Column<Personn, string>();
		public static readonly IColumn<Personn, string> LastName = new Column<Personn, string>();
		public static readonly IColumn<Personn, int> SecureCode = new NullableColumn<Personn, int>();
		public static readonly IColumn<Personn, string> Job = new NullableColumn<Personn, string>() { DefaultValue="No job"};
	}
}
