using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class OrganizationType: EFundraisingCRMDataObject {

		private short organizationTypeId;
		private short partyTypeId;
		private string organizationTypeDesc;
		private int isSchool;


		public OrganizationType() : this(short.MinValue) { }
		public OrganizationType(short organizationTypeId) : this(organizationTypeId, short.MinValue) { }
		public OrganizationType(short organizationTypeId, short partyTypeId) : this(organizationTypeId, partyTypeId, null) { }
		public OrganizationType(short organizationTypeId, short partyTypeId, string organizationTypeDesc) : this(organizationTypeId, partyTypeId, organizationTypeDesc, int.MinValue) { }
		public OrganizationType(short organizationTypeId, short partyTypeId, string organizationTypeDesc, int isSchool) {
			this.organizationTypeId = organizationTypeId;
			this.partyTypeId = partyTypeId;
			this.organizationTypeDesc = organizationTypeDesc;
			this.isSchool = isSchool;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationType>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<OrganizationTypeDesc>" + System.Web.HttpUtility.HtmlEncode(organizationTypeDesc) + "</OrganizationTypeDesc>\r\n" +
			"	<IsSchool>" + isSchool + "</IsSchool>\r\n" +
			"</OrganizationType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeDesc")) {
					SetXmlValue(ref organizationTypeDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isSchool")) {
					SetXmlValue(ref isSchool, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationType[] GetOrganizationTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationTypes();
		}

		
		public static string GetOrganizationTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationTypeByID(id);
		}
        /*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrganizationType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrganizationType(this);
		}*/
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

		public string OrganizationTypeDesc {
			set { organizationTypeDesc = value; }
			get { return organizationTypeDesc; }
		}

		public int IsSchool {
			set { isSchool = value; }
			get { return isSchool; }
		}

		#endregion
	}
}
