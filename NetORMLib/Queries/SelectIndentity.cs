using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;
using NetORMLib.Tables;

namespace NetORMLib.Queries
{
	public class SelectIdentity : ISelectIdentity
	{
		private ITable table;
		public ITable Table => table;

		public Action<object> ResultCallBack
		{
			get;
			private set;
		}

		public SelectIdentity(Action<object> ResultCallBack=null)
		{
			this.table = null;
			this.ResultCallBack = ResultCallBack;
		}

	
	}
}
