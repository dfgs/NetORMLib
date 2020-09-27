using NetORMLib;
using NetORMLib.Attributes;
using NetORMLib.Columns;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Tables
{

	public class PersonnTestName:Table
	{
		public static readonly IColumn<PersonnTestName, string> FirstNameExplicit = new Column<PersonnTestName, string>("FirstName");
		public static readonly IColumn<PersonnTestName, string> LastNameExplicit = new Column<PersonnTestName, string>("LastName");

	}
}
