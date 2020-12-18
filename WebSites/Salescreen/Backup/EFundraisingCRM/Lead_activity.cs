using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadActivity: EFundraisingCRMDataObject {

		private int leadActivityId;
		private int leadId;
		private int leadActivityTypeId;
		private DateTime leadActivityDate;
		private DateTime completedDate;
		private string comments;


		public LeadActivity() : this(int.MinValue) { }
		public LeadActivity(int leadActivityId) : this(leadActivityId, int.MinValue) { }
		public LeadActivity(int leadActivityId, int leadId) : this(leadActivityId, leadId, int.MinValue) { }
		public LeadActivity(int leadActivityId, int leadId, int leadActivityTypeId) : this(leadActivityId, leadId, leadActivityTypeId, DateTime.MinValue) { }
		public LeadActivity(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate) : this(leadActivityId, leadId, leadActivityTypeId, leadActivityDate, DateTime.MinValue) { }
		public LeadActivity(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate, DateTime completedDate) : this(leadActivityId, leadId, leadActivityTypeId, leadActivityDate, completedDate, null) { }
		public LeadActivity(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate, DateTime completedDate, string comments) {
			this.leadActivityId = leadActivityId;
			this.leadId = leadId;
			this.leadActivityTypeId = leadActivityTypeId;
			this.leadActivityDate = leadActivityDate;
			this.completedDate = completedDate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadActivity>\r\n" +
			"	<LeadActivityId>" + leadActivityId + "</LeadActivityId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<LeadActivityTypeId>" + leadActivityTypeId + "</LeadActivityTypeId>\r\n" +
			"	<LeadActivityDate>" + leadActivityDate + "</LeadActivityDate>\r\n" +
			"	<CompletedDate>" + completedDate + "</CompletedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</LeadActivity>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadActivityId")) {
					SetXmlValue(ref leadActivityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadActivityTypeId")) {
					SetXmlValue(ref leadActivityTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadActivityDate")) {
					SetXmlValue(ref leadActivityDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("completedDate")) {
					SetXmlValue(ref completedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadActivity[] GetLeadActivitys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivitys();
		}

		public static LeadActivity GetLeadActivityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadActivity(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadActivity(this);
		}
		#endregion

		#region Properties
		public int LeadActivityId {
			set { leadActivityId = value; }
			get { return leadActivityId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int LeadActivityTypeId {
			set { leadActivityTypeId = value; }
			get { return leadActivityTypeId; }
		}

		public DateTime LeadActivityDate {
			set { leadActivityDate = value; }
			get { return leadActivityDate; }
		}

		public DateTime CompletedDate {
			set { completedDate = value; }
			get { return completedDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
