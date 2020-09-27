using NetORMLib.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Tables
{
	public class JoinedTables : IJoinedTables
	{
		public ISingleTable FirstTable
		{
			get;
			private set;
		}

		private List<IJoin> joins;
		public IEnumerable<IJoin> Joins => joins;

		public JoinedTables(ISingleTable FirstTable,IJoin Join)
		{
			this.joins = new List<IJoin>();
			joins.Add(Join);
			this.FirstTable = FirstTable;
		}
		public IJoinedTables Join(IJoin Join)
		{
			this.joins.Add(Join);
			return this;
		}

		

	}
}
