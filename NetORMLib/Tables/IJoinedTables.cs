using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public interface IJoinedTables:ITable
	{
		ISingleTable FirstTable
		{
			get;
		}
		IEnumerable<IJoin> Joins
		{
			get;
		}

	}
}
