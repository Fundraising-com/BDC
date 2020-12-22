using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class OrganizationClass: EFundraisingCRMDataObject {

		private string organizationClassCode;
		private string description;
		private int acceptPO;
		private int isDistributor;


		public OrganizationClass() : this(null) { }
		public OrganizationClass(string organizationClassCode) : this(organizationClassCode, null) { }
		public OrganizationClass(string organizationClassCode, string description) : this(organizationClassCode, description, int.MinValue) { }
		public OrganizationClass(string organizationClassCode, string description, int acceptPO) : this(organizationClassCode, description, acceptPO, int.MinValue) { }
		public OrganizationClass(string organizationClassCode, string description, int acceptPO, int isDistributor) {
			this.organizationClassCode = organizationClassCode;
			this.description = description;
			this.acceptPO = acceptPO;
			this.isDistributor = isDistributor;
		}

		#region Static Data

		public OrganizationClass Distributor {
			get { return new OrganizationClass("DIST", "Distributor", 0, 1); }
		}

		public OrganizationClass Other {
			get { return new OrganizationClass("OTH", "Other", 0, 0); }
		}

		public OrganizationClass School {
			get { return new OrganizationClass("SC", "School", 1, 0); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrganizationClass>\r\n" +
			"	<OrganizationClassCode>" + System.Web.HttpUtility.HtmlEncode(organizationClassCode) + "</OrganizationClassCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<AcceptPO>" + acceptPO + "</AcceptPO>\r\n" +
			"	<IsDistributor>" + isDistributor + "</IsDistributor>\r\n" +
			"</OrganizationClass>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizationClassCode")) {
					SetXmlValue(ref organizationClassCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("acceptPo")) {
					SetXmlValue(ref acceptPO, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDistributor")) {
					SetXmlValue(ref isDistributor, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrganizationClass[] GetOrganizationClasss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationClasss();
		}

		/*
		public static OrganizationClass GetOrganizationClassByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationClassByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrganizationClass(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrganizationClass(this);
		}*/
		#endregion

		#region Properties
		public string OrganizationClassCode {
			set { organizationClassCode = value; }
			get { return organizationClassCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int AcceptPO {
			set { acceptPO = value; }
			get { return acceptPO; }
		}

		public int IsDistributor {
			set { isDistributor = value; }
			get { return isDistributor; }
		}

		#endregion
	}
}
