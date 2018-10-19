using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Databases
{
	public interface IDatabase
	{

		void BeginTransaction();
		void EndTransaction(bool Commit);

		IEnumerable<Row> Execute(ISelect Query);
		void Execute(IDelete Query);
		void Execute(IUpdate Query);

		IEnumerable<string> GetTables();

	}
}
