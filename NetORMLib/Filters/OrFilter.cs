using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class OrFilter : BooleanFilter, IOrFilter
	{
		
		public OrFilter(params IFilter[] Members):base(Members)
		{
		}

		

		public override string Format(IEnumerable<string> FormattedMembers)
		{
			return $"({String.Join(" OR ", FormattedMembers)})";
		}
		public  override IAndFilter And(IFilter Filter)
		{
			if (Filter is IAndFilter other)
			{
				other.Add(this);
				return other;
			}
			else
			{
				return new AndFilter(this, Filter);
			}
		}
		public override IOrFilter Or(IFilter Filter)
		{
			if (Filter is IOrFilter other)
			{
				foreach (IFilter item in other.Members) Add(item);
				return this;
			}
			else
			{
				Add(Filter);
				return this;
			}
		}
	}
}
