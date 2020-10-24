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
		private static UpgradeLogTable upgradeLogTable=new UpgradeLogTable();

		public VersionControl(IDatabase Database)
		{
			this.database = Database;
		}

		public int GetCurrentRevision()
		{
			IEnumerable<UpgradeLog> rows;

			if (!database.GetTables().Contains("UpgradeLog")) return -1;

			rows = database.Execute<UpgradeLog>(new Select(UpgradeLogTable.UpgradeLogID,UpgradeLogTable.Revision,UpgradeLogTable.Date));
			if (rows.Any()) return rows.Max(item => item.Revision );
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
				database.Execute(new CreateTable(upgradeLogTable, UpgradeLogTable.UpgradeLogID,UpgradeLogTable.Revision,UpgradeLogTable.Date));
				current = 0;
			}

			while(current<target)
			{
				current ++;
				upgradeLogInsert = new Insert().Into(upgradeLogTable).Set(UpgradeLogTable.Revision, current).Set(UpgradeLogTable.Date, DateTime.Now);
				database.Execute(OnUpgradeTo(current).Union( new IQuery[] { upgradeLogInsert} ));
					
			}


		}


	}
}
