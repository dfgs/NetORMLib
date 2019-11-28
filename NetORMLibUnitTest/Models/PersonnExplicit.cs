using NetORMLib;
using NetORMLib.Attributes;
using NetORMLib.Columns;

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
		public static readonly IColumn<PersonnExplicit, string> FirstNameExplicit = new Column<PersonnExplicit, string>("FirstName");
		public static readonly IColumn<PersonnExplicit, string> LastNameExplicit = new Column<PersonnExplicit, string>("LastName");

	}
}
