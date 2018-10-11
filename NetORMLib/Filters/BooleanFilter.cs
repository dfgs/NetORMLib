using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public abstract class BooleanFilter<T> : IBooleanFilter<T>
	{
		private IFilter<T>[] members;
		public IEnumerable<IFilter<T>> Members => members;
		IEnumerable<IFilter> IBooleanFilter.Members => members;
		
		public BooleanFilter(params IFilter<T>[] Members)
		{
			if (Members.Length < 2) throw new ArgumentException("Boolean filter must constain at least 2 members");
			this.members = Members;
		}

		public abstract string Format(IEnumerable<string> FormattedMembers);


	}
}
