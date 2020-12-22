using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PartnerPackages: EFundraisingCRMDataObject {

		private int partnerId;
		private short packageId;


		public PartnerPackages() : this(int.MinValue) { }
		public PartnerPackages(int partnerId) : this(partnerId, short.MinValue) { }
		public PartnerPackages(int partnerId, short packageId) {
			this.partnerId = partnerId;
			this.packageId = packageId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerPackages>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"</PartnerPackages>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerPackages[] GetPartnerPackagess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerPackagess();
		}

		public static PartnerPackages GetPartnerPackagesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerPackagesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerPackages(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerPackages(this);
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
