using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class OrFilter<T> : BooleanFilter<T>, IOrFilter<T>
	{
		
		public OrFilter(params IFilter<T>[] Members):base(Members)
		{
		}


		public override string Format(IEnumerable<string> FormattedMembers)
		{
			return $"({String.Join(" OR ", FormattedMembers)})";
		}


	}
}
