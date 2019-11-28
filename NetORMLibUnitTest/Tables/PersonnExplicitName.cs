using NetORMLib;
using NetORMLib.Attributes;
using NetORMLib.Columns;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Tables
{
	[Table(Name ="Personn")]
	public class PersonnExplicitName
	{
		public static readonly IColumn<PersonnExplicitName, string> FirstNameExplicit = new Column<PersonnExplicitName, string>("FirstName");
		public static readonly IColumn<PersonnExplicitName, string> LastNameExplicit = new Column<PersonnExplicitName, string>("LastName");

	}
}
