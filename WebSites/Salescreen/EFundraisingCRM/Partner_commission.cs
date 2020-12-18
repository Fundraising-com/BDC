using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PartnerCommission: EFundraisingCRMDataObject {

		private int partnerId;
		private short productClassId;
		private float commissionRate;


		public PartnerCommission() : this(int.MinValue) { }
		public PartnerCommission(int partnerId) : this(partnerId, short.MinValue) { }
		public PartnerCommission(int partnerId, short productClassId) : this(partnerId, productClassId, float.MinValue) { }
		public PartnerCommission(int partnerId, short productClassId, float commissionRate) {
			this.partnerId = partnerId;
			this.productClassId = productClassId;
			this.commissionRate = commissionRate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerCommission>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<CommissionRate>" + commissionRate + "</CommissionRate>\r\n" +
			"</PartnerCommission>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRate")) {
					SetXmlValue(ref commissionRate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerCommission[] GetPartnerCommissions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerCommissions();
		}

		public static PartnerCommission GetPartnerCommissionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerCommissionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerCommission(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerCommission(this);
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public float CommissionRate {
			set { commissionRate = value; }
			get { return commissionRate; }
		}

		#endregion
	}
}
