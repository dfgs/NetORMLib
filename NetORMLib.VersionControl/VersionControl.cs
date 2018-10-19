using NetORMLib.Databases;
using NetORMLib.Queries;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

		public int GetCurrentRevision()
		{
			IEnumerable<Row> rows;

			if (!database.GetTables().Contains("UpgradeLog")) return -1;

			rows = database.Execute(new Select().From<UpgradeLog>());
			if (rows.Any()) return rows.Max(item => ((dynamic)item).Revision);
			else return 0;
		}

		public abstract int GetTargetRevision();

		public bool IsUpToDate()
		{
			return GetTargetRevision() == GetCurrentRevision();
		}

		public void Upgrade()
		{
			int current;

			current = GetCurrentRevision();
			if (current==-1)
			{

			}
		}


	}
}
