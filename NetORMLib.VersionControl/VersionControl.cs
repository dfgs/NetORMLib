using NetORMLib.Databases;
using NetORMLib.Queries;
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

		public int GetCurrentRevision()
		{
			IEnumerable<UpgradeLogRow> rows;

			if (!database.GetTables().Contains("UpgradeLog")) return -1;

			rows = database.Execute<UpgradeLogRow>(new Select<UpgradeLog>(UpgradeLog.UpgradeLogID,UpgradeLog.Revision,UpgradeLog.Date));
			if (rows.Any()) return rows.Max(item => item.Revision);
			else return 0;
		}

		public abstract int GetTargetRevision();
		protected abstract IEnumerable<IQuery> OnUpgradeTo(int Version);

		public bool IsUpToDate()
		{
			return GetTargetRevision() == GetCurrentRevision();
		}

		public void Upgrade()
		{
			int current,target;
			IQuery upgradeLogInsert;

			target = GetTargetRevision();
			current = GetCurrentRevision();
			if (current==-1)
			{
				database.Execute(new CreateTable<UpgradeLog>(UpgradeLog.UpgradeLogID,UpgradeLog.Revision,UpgradeLog.Date));
				current = 0;
			}

			while(current<target)
			{
				current ++;
				upgradeLogInsert = new Insert<UpgradeLog>().Set(UpgradeLog.Revision, current).Set(UpgradeLog.Date, DateTime.Now);
				database.Execute(OnUpgradeTo(current).Union( new IQuery[] { upgradeLogInsert} ));
					
			}


		}


	}
}
