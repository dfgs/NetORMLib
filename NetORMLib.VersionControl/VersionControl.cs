using NetORMLib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.VersionControl
{
	public abstract class VersionControl : IVersionControl
	{
		private IDatabase database;

		public VersionControl(IDatabase Database)
		{
			this.database = Database;
		}

		public abstract int GetCurrentRevision();
		public abstract int GetTargetRevision();
		public abstract bool IsUpToDate();
		public abstract void Upgrade();
	}
}
