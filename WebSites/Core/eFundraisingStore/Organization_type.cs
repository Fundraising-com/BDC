using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class OrganizationType: eFundraisingStoreDataObject {

		private short organizationTypeId;
		private short partyTypeId;
		private string description;


		public OrganizationType() : this(short.MinValue) { }
		public OrganizationType(short organizationTypeId) : this(organizationTypeId, short.MinValue) { }
		public OrganizationType(short organizationTypeId, short partyTypeId) : this(organizationTypeId, partyTypeId, null) { }
		public OrganizationType(short organizationTypeId, short partyTypeId, string description) {
			this.organizationTypeId = organizationTypeId;
			this.partyTypeId = partyTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationType>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</OrganizationType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "organizationTypeId") {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "partyTypeId") {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationType[] GetOrganizationTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrganizationTypes();
		}

		public static OrganizationType GetOrganizationTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrganizationTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOrganizationType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOrganizationType(this);
		}
		#endregion

		#region Properties
		public short OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
