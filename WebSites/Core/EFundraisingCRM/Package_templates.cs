using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PackageTemplates: EFundraisingCRMDataObject {

		private short packageTemplateId;
		private string packageTemplateDesc;


		public PackageTemplates() : this(short.MinValue) { }
		public PackageTemplates(short packageTemplateId) : this(packageTemplateId, null) { }
		public PackageTemplates(short packageTemplateId, string packageTemplateDesc) {
			this.packageTemplateId = packageTemplateId;
			this.packageTemplateDesc = packageTemplateDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PackageTemplates>\r\n" +
			"	<PackageTemplateId>" + packageTemplateId + "</PackageTemplateId>\r\n" +
			"	<PackageTemplateDesc>" + System.Web.HttpUtility.HtmlEncode(packageTemplateDesc) + "</PackageTemplateDesc>\r\n" +
			"</PackageTemplates>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("packageTemplateId")) {
					SetXmlValue(ref packageTemplateId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageTemplateDesc")) {
					SetXmlValue(ref packageTemplateDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PackageTemplates[] GetPackageTemplatess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackageTemplatess();
		}

		/*
		public static PackageTemplates GetPackageTemplatesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackageTemplatesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPackageTemplates(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePackageTemplates(this);
		}*/
		#endregion

		#region Properties
		public short PackageTemplateId {
			set { packageTemplateId = value; }
			get { return packageTemplateId; }
		}

		public string PackageTemplateDesc {
			set { packageTemplateDesc = value; }
			get { return packageTemplateDesc; }
		}

		#endregion
	}
}
