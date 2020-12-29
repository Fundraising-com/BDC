using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerPackage: eFundraisingStoreDataObject {

		private int partnerId;
		private short packageId;


		public PartnerPackage() : this(int.MinValue) { }
		public PartnerPackage(int partnerId) : this(partnerId, short.MinValue) { }
		public PartnerPackage(int partnerId, short packageId) {
			this.partnerId = partnerId;
			this.packageId = packageId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerPackage>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"</PartnerPackage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "packageId") {
					SetXmlValue(ref packageId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerPackage[] GetPartnerPackages() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerPackages();
		}

		public static PartnerPackage GetPartnerPackageByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerPackageByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerPackage(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerPackage(this);
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		#endregion
	}
}
