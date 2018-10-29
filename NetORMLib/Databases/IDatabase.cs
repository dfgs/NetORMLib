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

		/*void BeginTransaction();
		void EndTransaction(bool Commit);*/

		IEnumerable<Row> Execute(ISelect Query);
		object Execute(ISelectIdentity Query);
		void Execute(IQuery Query);
		void Execute(params IQuery[] Queries);
		void Execute(IEnumerable<IQuery> Queries);

		IEnumerable<string> GetTables();

	}
}
