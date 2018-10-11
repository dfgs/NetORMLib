using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class AndFilter<T> : IFilter<T>
	{
		private IFilter<T>[] members;
		public IEnumerable<IFilter<T>> Members
		{
			get { return members; }
		}
		public AndFilter(params IFilter<T>[] Members)
		{
			if (Members.Length < 2) throw new ArgumentException("And filter must constain at least 2 members");
			this.members = Members;
		}

	}
}
