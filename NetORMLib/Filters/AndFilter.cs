﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Filters
{
	public class AndFilter<T> : BooleanFilter<T>, IAndFilter<T>
	{
		
		public AndFilter(params IFilter<T>[] Members):base(Members)
		{
		}


		public override string Format(IEnumerable<string> FormattedMembers)
		{
			return $"({String.Join(" AND ", FormattedMembers)})";
		}


	}
}
