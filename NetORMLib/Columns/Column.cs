using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using NetORMLib.Filters;



namespace NetORMLib.Columns
{
	public class Column<T,TVal>:BaseColumn<T,TVal>
	{
		
	
		public override bool IsNullable
		{
			get;
			set;
		}

		

		

		public Column([CallerMemberName]string Name=null):base(Name)
		{
		}
			   
		


	}
}
