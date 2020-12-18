using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadActivityCopy: EFundraisingCRMDataObject {

		private int leadActivityId;
		private int leadId;
		private int leadActivityTypeId;
		private DateTime leadActivityDate;
		private DateTime completedDate;
		private string comments;


		public LeadActivityCopy() : this(int.MinValue) { }
		public LeadActivityCopy(int leadActivityId) : this(leadActivityId, int.MinValue) { }
		public LeadActivityCopy(int leadActivityId, int leadId) : this(leadActivityId, leadId, int.MinValue) { }
		public LeadActivityCopy(int leadActivityId, int leadId, int leadActivityTypeId) : this(leadActivityId, leadId, leadActivityTypeId, DateTime.MinValue) { }
		public LeadActivityCopy(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate) : this(leadActivityId, leadId, leadActivityTypeId, leadActivityDate, DateTime.MinValue) { }
		public LeadActivityCopy(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate, DateTime completedDate) : this(leadActivityId, leadId, leadActivityTypeId, leadActivityDate, completedDate, null) { }
		public LeadActivityCopy(int leadActivityId, int leadId, int leadActivityTypeId, DateTime leadActivityDate, DateTime completedDate, string comments) {
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
			return "<LeadActivityCopy>\r\n" +
			"	<LeadActivityId>" + leadActivityId + "</LeadActivityId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<LeadActivityTypeId>" + leadActivityTypeId + "</LeadActivityTypeId>\r\n" +
			"	<LeadActivityDate>" + leadActivityDate + "</LeadActivityDate>\r\n" +
			"	<CompletedDate>" + completedDate + "</CompletedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</LeadActivityCopy>\r\n";
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
		public static LeadActivityCopy[] GetLeadActivityCopys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityCopys();
		}

		public static LeadActivityCopy GetLeadActivityCopyByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityCopyByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadActivityCopy(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadActivityCopy(this);
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
