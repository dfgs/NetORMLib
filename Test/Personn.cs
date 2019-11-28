using NetORMLib.Columns;


namespace Test
{
	public class Personn
	{
		public static readonly IColumn<Personn, string> FirstName = new Column<Personn, string>() { IsIdentity=true,IsPrimaryKey=true };
		
		public static readonly IColumn<Personn, string> LastName = new Column<Personn, string>();

	}
}
