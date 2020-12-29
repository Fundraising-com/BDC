using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for PrizeMemberReport.
	/// </summary>
	public class PrizeMemberReport {
		private ArrayList prizeMember;

		public PrizeMemberReport() {
			prizeMember = new ArrayList();
		}

		public static PrizeMemberReport Create(eSubsGlobalEnvironment env) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeMemberReport(env.Event.EventID);
		}

		public void Reset(PrizeMemberRow row) {
			if(row.EventParticipantionID == int.MinValue) {
				row.EventParticipantionID = 0;
			}

			if(row.FirstName == null) {
				row.FirstName = "";
			}

			if(row.LastName == null) {
				row.LastName = "";
			}

			if(row.NumberOfSubs == int.MinValue) {
				row.NumberOfSubs = 0;				
			}

			if(row.NumberOfSupporters == int.MinValue) {
				row.NumberOfSupporters = 0;
			}
	}

		public void AddPrizeMemberReport(PrizeMemberRow row) {
			Reset(row);
			prizeMember.Add(row);
		}

		public ArrayList PrizeMembers {
			get { return prizeMember; }
		}
	}
}
