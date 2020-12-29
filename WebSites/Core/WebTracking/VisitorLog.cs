//
//	December 9 2005 : Maxime Normand - Added VisitorLog(int websiteid) contructor and
//					  CreateFromWebSiteID() method to use with efundraising.com
//

using System;

namespace GA.BDC.Core.WebTracking {
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    [Serializable]
   public class VisitorLog : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private int visitorLogID = int.MinValue;
		private int webSiteID = int.MinValue;
		private int hostID = int.MinValue;
		private int leadID = int.MinValue;
		private string version;
		private int promotionID = int.MinValue;
		private int touchID = int.MinValue;
		private int tellAFriendID = int.MinValue;
		private string visitorGUID;
		private DateTime createDate;
		private int partnerID = int.MinValue;
		private string extSiteID = null;

		private int _increment = 0;
		private int _trackNo = 0;

		public VisitorLog() {
			hostID = Config.HostID;
			webSiteID = Config.WebSiteID;
		}
		
		public VisitorLog(int websiteid) {
			hostID  = Config.HostID;
			webSiteID = websiteid;
		}

		public static VisitorLog Create(System.Web.SessionState.HttpSessionState session) {
			if(session["visitor_log"] != null) {
				return (VisitorLog)session["visitor_log"];
			} else {
				return new VisitorLog();
			}
		}
		
		public static VisitorLog CreateFromWebSiteID(System.Web.SessionState.HttpSessionState session, int websiteid) {
			if(session["visitor_log"] != null) {
				return (VisitorLog)session["visitor_log"];
			} else {
				return new VisitorLog(websiteid);
			}
		}

		public void IncrementEvent() {
			_increment++;
		}

		public void IncrementClick() {
			_trackNo++;
		}

		public void Save(System.Web.SessionState.HttpSessionState session) {
			session["visitor_log"] = this;
		}

		public void InsertIntoDatabase() {
			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
			if(visitorGUID == null || visitorGUID == "") {
				visitorGUID = dbo.GetVisitorGUID();
			}
			visitorLogID = dbo.InsertVisitorLog(webSiteID, hostID, leadID, version, promotionID, touchID, visitorGUID, tellAFriendID, this.extSiteID);
			if(visitorLogID < 0) {
				throw new Exception("Unable to insert visitor log");
			}
		}

		public void UpdateVisitorLog(int leadId) {
			DataAccess.WebTrackingDatabase dbo = new GA.BDC.Core.WebTracking.DataAccess.WebTrackingDatabase();
			if(visitorLogID > -1 && leadId > -1) {
				this.LeadID = leadId;
				dbo.UpdateVisitorLog(visitorLogID, leadId);				
			}
		}

		public void UpdatePartnerID() {
			if(visitorLogID > -1 && partnerID > -1) {
				DataAccess.WebTrackingDatabase dbo = new GA.BDC.Core.WebTracking.DataAccess.WebTrackingDatabase();
				dbo.UpdateWebSiteFromPartnerID(partnerID, 1, visitorLogID);
			}
		}

		public static System.Data.DataSet GetDataSetVisitorSteps(int visitorLogID, int hierarchyID) {
			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
			return dbo.GetVisitorSteps(visitorLogID, hierarchyID);
		}

		#region Properties
		public int VisitorLogID {
			set { visitorLogID = value; }
			get { return visitorLogID; }
		}

		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public int WebSiteID {
			set { webSiteID = value; }
			get { return webSiteID; }
		}

		public int TellAFriendID {
			set { tellAFriendID = value; }
			get { return tellAFriendID; }
		}

		public int HostID {
			set { hostID = value; }
			get { return hostID; }
		}

		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public string Version {
			set { version = value; }
			get { return version; }
		}

		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public int TouchID {
			set { touchID = value; }
			get { return touchID; }
		}

		public string VisitorGUID {
			set { visitorGUID = value; }
			get { return visitorGUID; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		public int Increment {
			set { _increment = value; }
			get { return _increment; }
		}

		public int TrackNo {
			set { _trackNo = value; }
			get { return _trackNo; }
		}

		public string ExternalSiteID
		{
			set { this.extSiteID = value;}
			get { return this.extSiteID;}
		}
		#endregion
	}
}
