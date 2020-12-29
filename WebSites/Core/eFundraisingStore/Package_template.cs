using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PackageTemplate: eFundraisingStoreDataObject {

		private short packageTemplateId;
		private string packageTemplateDesc;


		public PackageTemplate() : this(short.MinValue) { }
		public PackageTemplate(short packageTemplateId) : this(packageTemplateId, null) { }
		public PackageTemplate(short packageTemplateId, string packageTemplateDesc) {
			this.packageTemplateId = packageTemplateId;
			this.packageTemplateDesc = packageTemplateDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PackageTemplate>\r\n" +
			"	<PackageTemplateId>" + packageTemplateId + "</PackageTemplateId>\r\n" +
			"	<PackageTemplateDesc>" + System.Web.HttpUtility.HtmlEncode(packageTemplateDesc) + "</PackageTemplateDesc>\r\n" +
			"</PackageTemplate>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "packageTemplateId") {
					SetXmlValue(ref packageTemplateId, node.InnerText);
				}
				if(node.Name.ToLower() == "packageTemplateDesc") {
					SetXmlValue(ref packageTemplateDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
	/*	
		public static PackageTemplate[] GetPackageTemplates() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageTemplates();
		}

		public static PackageTemplate GetPackageTemplateByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageTemplateByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPackageTemplate(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePackageTemplate(this);
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
