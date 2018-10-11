using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class TableAttribute:Attribute
	{
		public string Name
		{
			get;
			set;
		}
	}
}
