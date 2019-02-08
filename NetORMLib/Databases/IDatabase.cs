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

		IEnumerable<T> Execute<T>(ISelect Query) where T:new();
		object Execute(ISelectIdentity Query);
		void Execute(IQuery Query);
		void Execute(params IQuery[] Queries);
		void Execute(IEnumerable<IQuery> Queries);

		IEnumerable<string> GetTables();

	}
}
