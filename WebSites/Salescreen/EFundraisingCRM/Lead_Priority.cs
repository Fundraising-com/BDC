using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LeadPriority: EFundraisingCRMDataObject {

		private int leadPriorityId;
		private string description;


		public LeadPriority() : this(int.MinValue) { }
		public LeadPriority(int leadPriorityId) : this(leadPriorityId, null) { }
		public LeadPriority(int leadPriorityId, string description) {
			this.leadPriorityId = leadPriorityId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadPriority>\r\n" +
			"	<LeadPriorityId>" + leadPriorityId + "</LeadPriorityId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LeadPriority>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadPriorityId")) {
					SetXmlValue(ref leadPriorityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadPriority[] GetLeadPrioritys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPrioritys();
		}

		public static LeadPriority GetLeadPriorityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPriorityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadPriority(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadPriority(this);
		}
		#endregion

		#region Properties
		public int LeadPriorityId {
			set { leadPriorityId = value; }
			get { return leadPriorityId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
