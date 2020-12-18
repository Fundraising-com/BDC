using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadActivityClosingReason: EFundraisingCRMDataObject {

		private short activityClosingReasonID;
		private string reason;


		public LeadActivityClosingReason() : this(short.MinValue) { }
		public LeadActivityClosingReason(short activityClosingReasonID) : this(activityClosingReasonID, null) { }
		public LeadActivityClosingReason(short activityClosingReasonID, string reason) {
			this.activityClosingReasonID = activityClosingReasonID;
			this.reason = reason;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadActivityClosingReason>\r\n" +
			"	<ActivityClosingReasonID>" + activityClosingReasonID + "</ActivityClosingReasonID>\r\n" +
			"	<Reason>" + System.Web.HttpUtility.HtmlEncode(reason) + "</Reason>\r\n" +
			"</LeadActivityClosingReason>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("activityClosingReasonId")) {
					SetXmlValue(ref activityClosingReasonID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("reason")) {
					SetXmlValue(ref reason, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadActivityClosingReason[] GetLeadActivityClosingReasons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityClosingReasons();
		}

		/*
		public static LeadActivityClosingReason GetLeadActivityClosingReasonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityClosingReasonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadActivityClosingReason(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadActivityClosingReason(this);
		}*/
		#endregion

		#region Properties
		public short ActivityClosingReasonID {
			set { activityClosingReasonID = value; }
			get { return activityClosingReasonID; }
		}

		public string Reason {
			set { reason = value; }
			get { return reason; }
		}

		#endregion
	}
}
