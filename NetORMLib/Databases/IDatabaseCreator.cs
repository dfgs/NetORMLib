using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Databases
{
	public interface IDatabaseCreator
	{
		string DatabaseName
		{
			get;
		}
		bool DatabaseExists();
		void CreateDatabase();
		void DropDatabase();
	}
}
