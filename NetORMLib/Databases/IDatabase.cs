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

		IEnumerable<TRow> Execute<TRow>(ISelect Query)
			where TRow:new();
		object Execute(ISelectIdentity Query);
		void Execute(IQuery Query);
		object Execute(params IQuery[] Queries);
		object Execute(IEnumerable<IQuery> Queries);

		IEnumerable<string> GetTables();

	}
}
