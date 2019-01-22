using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Queries
{
	public interface ICallBackQuery:IQuery
	{
		Action<object> ResultCallBack
		{
			get;
		}
	}
	/*public interface ICallBackQuery<T>:IQuery<T>
	{

	}*/
}
