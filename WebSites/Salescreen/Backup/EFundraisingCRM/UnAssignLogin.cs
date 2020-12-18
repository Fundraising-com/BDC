using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class UnAssignLogin: EFundraisingCRMDataObject {

		private int unAssignLoginId;
		private string userName;
		private int consultantId;
		private int leadId;
		private DateTime unassignmentTimeStamp;


		public UnAssignLogin() : this(int.MinValue) { }
		public UnAssignLogin(int unAssignLoginId) : this(unAssignLoginId, null) { }
		public UnAssignLogin(int unAssignLoginId, string userName) : this(unAssignLoginId, userName, int.MinValue) { }
		public UnAssignLogin(int unAssignLoginId, string userName, int consultantId) : this(unAssignLoginId, userName, consultantId, int.MinValue) { }
		public UnAssignLogin(int unAssignLoginId, string userName, int consultantId, int leadId) : this(unAssignLoginId, userName, consultantId, leadId, DateTime.MinValue) { }
		public UnAssignLogin(int unAssignLoginId, string userName, int consultantId, int leadId, DateTime unassignmentTimeStamp) {
			this.unAssignLoginId = unAssignLoginId;
			this.userName = userName;
			this.consultantId = consultantId;
			this.leadId = leadId;
			this.unassignmentTimeStamp = unassignmentTimeStamp;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<UnAssignLogin>\r\n" +
			"	<UnAssignLoginId>" + unAssignLoginId + "</UnAssignLoginId>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<UnassignmentTimeStamp>" + unassignmentTimeStamp + "</UnassignmentTimeStamp>\r\n" +
			"</UnAssignLogin>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("unAssignLoginId")) {
					SetXmlValue(ref unAssignLoginId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unassignmentTimestamp")) {
					SetXmlValue(ref unassignmentTimeStamp, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static UnAssignLogin[] GetUnAssignLogins() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnAssignLogins();
		}

		public static UnAssignLogin GetUnAssignLoginByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnAssignLoginByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertUnAssignLogin(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateUnAssignLogin(this);
		}
		#endregion

		#region Properties
		public int UnAssignLoginId {
			set { unAssignLoginId = value; }
			get { return unAssignLoginId; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public int ConsultantId {
			set { consultantId = value; }
			get { return consultantId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public DateTime UnassignmentTimeStamp {
			set { unassignmentTimeStamp = value; }
			get { return unassignmentTimeStamp; }
		}

		#endregion
	}
}
