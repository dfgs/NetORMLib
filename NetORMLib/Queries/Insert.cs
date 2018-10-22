using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class Insert<T> : IInsert
	{
		private string table;
		public string Table => table;


		private IEnumerable<ISetter> setters;
		public IEnumerable<ISetter> Setters => setters;

		public Insert(params ISetter<T>[] Setters)
		{
			if ((Setters == null) || (Setters.Length == 0)) throw new ArgumentNullException("Must must specify at least one setter");
			setters = Setters;
			table = Table<T>.Name;
		}



		


	}
}
