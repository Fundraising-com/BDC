using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class OrganizationStatus: EFundraisingCRMDataObject {

		private int organizationStatusID;
		private string description;


		public OrganizationStatus() : this(int.MinValue) { }
		public OrganizationStatus(int organizationStatusID) : this(organizationStatusID, null) { }
		public OrganizationStatus(int organizationStatusID, string description) {
			this.organizationStatusID = organizationStatusID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationStatus>\r\n" +
			"	<OrganizationStatusID>" + organizationStatusID + "</OrganizationStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</OrganizationStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizationStatusId")) {
					SetXmlValue(ref organizationStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationStatus[] GetOrganizationStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationStatuss();
		}

		public static OrganizationStatus GetOrganizationStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrganizationStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrganizationStatus(this);
		}
		#endregion

		#region Properties
		public int OrganizationStatusID {
			set { organizationStatusID = value; }
			get { return organizationStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
