using NetORMLib;
using NetORMLib.Columns;
using NetORMLib.DbTypes;
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
		public static readonly IColumn<Personn, DbInt> PersonnID = new Column<Personn, DbInt>() { IsPrimaryKey=true,IsIdentity=true };
		public static readonly IColumn<Personn, DbString> FirstName = new Column<Personn, DbString>();
		public static readonly IColumn<Personn, DbString> LastName = new Column<Personn, DbString>();
		public static readonly IColumn<Personn, DbInt> SecureCode = new NullableColumn<Personn, DbInt>();
		public static readonly IColumn<Personn, DbString> Job = new NullableColumn<Personn, DbString>();
}
}
