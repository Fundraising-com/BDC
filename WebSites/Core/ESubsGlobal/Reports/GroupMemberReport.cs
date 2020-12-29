using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for GroupMemberReport.
	/// </summary>
    [Serializable]
	public class GroupMemberReport {
		private ArrayList members;

		public GroupMemberReport() {
			members = new ArrayList();
		}

		public static GroupMemberReport Create(eSubsGlobalEnvironment env) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupMemberReport(env.Event.EventID);
		}

		public static GroupMemberReport Create(int eventID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupMemberReport(eventID);
		}

		public void AddGroupMemberRow(GroupMemberRow row) {
			if(row.Amount == Decimal.MinValue) {
				row.Amount = 0;
			}
			if(row.EmailSent == int.MinValue) {
				row.EmailSent = 0;
			}
			if(row.MemberName == null) {
				row.MemberName = "";
			}
			if(row.NumberOfSubs == int.MinValue) {
				row.NumberOfSubs = 0;
			}
			if(row.Profit == Decimal.MinValue) {
				row.Profit = 0;
			}
			members.Add(row);
		}
		
		public ArrayList Members {
			get { return members; }
		}
	}
}
