using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Databases
{
	public abstract class DatabaseCreator : IDatabaseCreator
	{
		public string DatabaseName
		{
			get;
			private set;
		}


		public DatabaseCreator(string DatabaseName)
		{
			this.DatabaseName = DatabaseName;
		}

		public abstract void CreateDatabase();
		public abstract void DropDatabase();
		public abstract bool DatabaseExists();
	}
}
