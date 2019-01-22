using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetORMLib.Columns;
using NetORMLib.Filters;

namespace NetORMLib.Queries
{
	public class SelectIdentity<T> : ISelectIdentity<T>
	{
		public string Table => Table<T>.Name;

		public Action<object> ResultCallBack
		{
			get;
			private set;
		}

		public SelectIdentity(Action<object> ResultCallBack=null)
		{
			this.ResultCallBack = ResultCallBack;
		}

	
	}
}
