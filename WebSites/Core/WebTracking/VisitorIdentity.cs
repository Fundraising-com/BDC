using System;

namespace GA.BDC.Core.WebTracking {
	/// <summary>
	/// Summary description for VisitorIdentity.
	/// </summary>
    [Serializable]
   public class VisitorIdentity : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private int visitorLogID;
		private int memberHierarchyID;
		private DateTime createDate;

		public VisitorIdentity(VisitorLog vl) {
			visitorLogID = vl.VisitorLogID;
		}

		public void InsertIntoDatabase() {
			if(visitorLogID > 0) {
				DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
				dbo.InsertVisitorIdentity(visitorLogID, memberHierarchyID);
			}
		}

		public int VisitorLogID {
			set { visitorLogID = value; }
			get { return visitorLogID; }
		}

		public int MemberHierarchyID {
			set { memberHierarchyID = value; }
			get { return memberHierarchyID; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

	}
}
