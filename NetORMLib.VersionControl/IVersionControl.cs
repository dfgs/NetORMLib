using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public interface IVersionControl
	{
		int GetCurrentRevision();
		int GetTargetRevision();
		bool IsUpToDate();
		void Upgrade();
	}
}
