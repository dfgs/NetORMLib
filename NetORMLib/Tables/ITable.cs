using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public interface ITable
	{
		IJoinedTables Join(IJoin Join);
	}
}
