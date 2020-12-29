using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadActivityType: EFundraisingCRMDataObject {

		private int leadActivityTypeId;
		private string description;
		private int priority;


		public LeadActivityType() : this(int.MinValue) { }
		public LeadActivityType(int leadActivityTypeId) : this(leadActivityTypeId, null) { }
		public LeadActivityType(int leadActivityTypeId, string description) : this(leadActivityTypeId, description, int.MinValue) { }
		public LeadActivityType(int leadActivityTypeId, string description, int priority) {
			this.leadActivityTypeId = leadActivityTypeId;
			this.description = description;
			this.priority = priority;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadActivityType>\r\n" +
			"	<LeadActivityTypeId>" + leadActivityTypeId + "</LeadActivityTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Priority>" + priority + "</Priority>\r\n" +
			"</LeadActivityType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadActivityTypeId")) {
					SetXmlValue(ref leadActivityTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("priority")) {
					SetXmlValue(ref priority, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadActivityType[] GetLeadActivityTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityTypes();
		}

		public static LeadActivityType GetLeadActivityTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadActivityTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadActivityType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadActivityType(this);
		}
		#endregion

		#region Properties
		public int LeadActivityTypeId {
			set { leadActivityTypeId = value; }
			get { return leadActivityTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int Priority {
			set { priority = value; }
			get { return priority; }
		}

		#endregion
	}
}
