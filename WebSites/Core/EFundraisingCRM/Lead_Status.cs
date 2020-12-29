using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadStatus: EFundraisingCRMDataObject {

		private int leadStatusID;
		private string description;


		public LeadStatus() : this(int.MinValue) { }
		public LeadStatus(int leadStatusID) : this(leadStatusID, null) { }
		public LeadStatus(int leadStatusID, string description) {
			this.leadStatusID = leadStatusID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadStatus>\r\n" +
			"	<LeadStatusID>" + leadStatusID + "</LeadStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LeadStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadStatusId")) {
					SetXmlValue(ref leadStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadStatus[] GetLeadStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadStatuss();
		}

		public static LeadStatus GetLeadStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadStatus(this);
		}
		#endregion

		#region Properties
		public int LeadStatusID {
			set { leadStatusID = value; }
			get { return leadStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
