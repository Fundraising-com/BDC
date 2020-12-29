using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SSDropBoxPackage: EFundraisingCRMDataObject {

		private int sSDropBoxId;
		private int packageId;
		private int displayOrder;


		public SSDropBoxPackage() : this(int.MinValue) { }
		public SSDropBoxPackage(int sSDropBoxId) : this(sSDropBoxId, int.MinValue) { }
		public SSDropBoxPackage(int sSDropBoxId, int packageId) : this(sSDropBoxId, packageId, int.MinValue) { }
		public SSDropBoxPackage(int sSDropBoxId, int packageId, int displayOrder) {
			this.sSDropBoxId = sSDropBoxId;
			this.packageId = packageId;
			this.displayOrder = displayOrder;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SSDropBoxPackage>\r\n" +
			"	<SSDropBoxId>" + sSDropBoxId + "</SSDropBoxId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"</SSDropBoxPackage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("ssDropBoxId")) {
					SetXmlValue(ref sSDropBoxId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SSDropBoxPackage[] GetSSDropBoxPackages() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSSDropBoxPackages();
		}

		public static SSDropBoxPackage GetSSDropBoxPackageByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSSDropBoxPackageByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSSDropBoxPackage(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSSDropBoxPackage(this);
		}
		#endregion

		#region Properties
		public int SSDropBoxId {
			set { sSDropBoxId = value; }
			get { return sSDropBoxId; }
		}

		public int PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public int DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		#endregion
	}
}
