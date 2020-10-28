using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLibUnitTest.Tables
{
	public static class TestDB
	{
		public static JobTable JobTable = new JobTable();
		public static JobTypeTable JobTypeTable = new JobTypeTable();
		public static PersonnExplicitName PersonnExplicitName = new PersonnExplicitName();
		public static PersonnTable PersonnTable = new PersonnTable();
		public static EnumTable EnumTable= new EnumTable();
		public static PersonnTestName PersonnTestName = new PersonnTestName();
		public static ChildTable ChildTable = new ChildTable();
	}
}
