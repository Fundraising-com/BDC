using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class OrganizationTypeDesc: EFundraisingCRMDataObject {

		private short organizationTypeId;
		private short languageId;
		private string description;


		public OrganizationTypeDesc() : this(short.MinValue) { }
		public OrganizationTypeDesc(short organizationTypeId) : this(organizationTypeId, short.MinValue) { }
		public OrganizationTypeDesc(short organizationTypeId, short languageId) : this(organizationTypeId, languageId, null) { }
		public OrganizationTypeDesc(short organizationTypeId, short languageId, string description) {
			this.organizationTypeId = organizationTypeId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationTypeDesc>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</OrganizationTypeDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationTypeDesc[] GetOrganizationTypeDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationTypeDescs();
		}

		/*
		public static OrganizationTypeDesc GetOrganizationTypeDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationTypeDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrganizationTypeDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrganizationTypeDesc(this);
		}*/
		#endregion

		#region Properties
		public short OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
