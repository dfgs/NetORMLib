using NetORMLib.Columns;
using NetORMLib.DbTypes;

namespace Test
{
	public class Personn
	{
		public static readonly IColumn<Personn, DbString> FirstName = new Column<Personn, DbString>();
		
		public static readonly IColumn<Personn, DbString> LastName = new Column<Personn, DbString>();

	}
}
