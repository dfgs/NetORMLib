using NetORMLib;
using NetORMLib.Attributes;
using NetORMLib.Columns;
using NetORMLib.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Models
{
	[Table(Name ="Personn")]
	public class PersonnExplicit
	{
		public static readonly IColumn<PersonnExplicit, DbString> FirstNameExplicit = new Column<PersonnExplicit, DbString>("FirstName");
		public static readonly IColumn<PersonnExplicit, DbString> LastNameExplicit = new Column<PersonnExplicit, DbString>("LastName");

	}
}
