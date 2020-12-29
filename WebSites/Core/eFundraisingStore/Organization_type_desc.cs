using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class OrganizationTypeDesc: eFundraisingStoreDataObject {

		private short organizationTypeId;
		private string cultureCode;
		private string description;


		public OrganizationTypeDesc() : this(short.MinValue) { }
		public OrganizationTypeDesc(short organizationTypeId) : this(organizationTypeId, null) { }
		public OrganizationTypeDesc(short organizationTypeId, string cultureCode) : this(organizationTypeId, cultureCode, null) { }
		public OrganizationTypeDesc(short organizationTypeId, string cultureCode, string description) {
			this.organizationTypeId = organizationTypeId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationTypeDesc>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</OrganizationTypeDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "organizationTypeId") {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationTypeDesc[] GetOrganizationTypeDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrganizationTypeDescs();
		}

		public static OrganizationTypeDesc GetOrganizationTypeDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrganizationTypeDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOrganizationTypeDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOrganizationTypeDesc(this);
		}
		#endregion

		#region Properties
		public short OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
