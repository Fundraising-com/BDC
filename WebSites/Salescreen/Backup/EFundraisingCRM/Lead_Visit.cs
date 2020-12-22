using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadVisit: EFundraisingCRMDataObject {

		private int leadVisitID;
		private int promotionID;
		private int leadID;
		private int tempLeadID;
		private DateTime visitDate;
		private string channelCode;


		public LeadVisit() : this(int.MinValue) { }
		public LeadVisit(int leadVisitID) : this(leadVisitID, int.MinValue) { }
		public LeadVisit(int leadVisitID, int promotionID) : this(leadVisitID, promotionID, int.MinValue) { }
		public LeadVisit(int leadVisitID, int promotionID, int leadID) : this(leadVisitID, promotionID, leadID, int.MinValue) { }
		public LeadVisit(int leadVisitID, int promotionID, int leadID, int tempLeadID) : this(leadVisitID, promotionID, leadID, tempLeadID, DateTime.MinValue) { }
		public LeadVisit(int leadVisitID, int promotionID, int leadID, int tempLeadID, DateTime visitDate) : this(leadVisitID, promotionID, leadID, tempLeadID, visitDate, null) { }
		public LeadVisit(int leadVisitID, int promotionID, int leadID, int tempLeadID, DateTime visitDate, string channelCode) {
			this.leadVisitID = leadVisitID;
			this.promotionID = promotionID;
			this.leadID = leadID;
			this.tempLeadID = tempLeadID;
			this.visitDate = visitDate;
			this.channelCode = channelCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadVisit>\r\n" +
			"	<LeadVisitID>" + leadVisitID + "</LeadVisitID>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"	<LeadID>" + leadID + "</LeadID>\r\n" +
			"	<TempLeadID>" + tempLeadID + "</TempLeadID>\r\n" +
			"	<VisitDate>" + visitDate + "</VisitDate>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"</LeadVisit>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadVisitId")) {
					SetXmlValue(ref leadVisitID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tempLeadId")) {
					SetXmlValue(ref tempLeadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("visitDate")) {
					SetXmlValue(ref visitDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channelCode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadVisit[] GetLeadVisits() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadVisits();
		}

		public static LeadVisit GetLeadVisitByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadVisitByID(id);
		}
		public static LeadVisit GetLastLeadVisitByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLastLeadVisitByLeadID(leadID);
		}
		
		public static LeadVisit[] GetLeadVisitsByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadVisitsByLeadID(leadID);
		}

		public static LeadVisit[] GetLeadVisitWithoutPromoKiit()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadVisitWithoutPromoKit();
		}
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadVisit(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadVisit(this);
		}
		#endregion

		#region Properties
		public int LeadVisitID {
			set { leadVisitID = value; }
			get { return leadVisitID; }
		}

		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public int TempLeadID {
			set { tempLeadID = value; }
			get { return tempLeadID; }
		}

		public DateTime VisitDate {
			set { visitDate = value; }
			get { return visitDate; }
		}

		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		#endregion
		#region Static Public Methods
//		public static LeadVisit[] GetLeadVisitWithoutPromotionalKit()
//		{
//		}
		#endregion
	}
}
