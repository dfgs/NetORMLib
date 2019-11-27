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

		IEnumerable<Row<T>> Execute<T>(ISelect Query);
		object Execute(ISelectIdentity Query);
		void Execute(IQuery Query);
		void Execute(params IQuery[] Queries);
		void Execute(IEnumerable<IQuery> Queries);

		IEnumerable<string> GetTables();

	}
}
