using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SaleCarrierShippingStatus: EFundraisingCRMDataObject {

		private short carrierShippingStatusId;
		private int salesId;
		private DateTime statusEntryDate;


		public SaleCarrierShippingStatus() : this(short.MinValue) { }
		public SaleCarrierShippingStatus(short carrierShippingStatusId) : this(carrierShippingStatusId, int.MinValue) { }
		public SaleCarrierShippingStatus(short carrierShippingStatusId, int salesId) : this(carrierShippingStatusId, salesId, DateTime.MinValue) { }
		public SaleCarrierShippingStatus(short carrierShippingStatusId, int salesId, DateTime statusEntryDate) {
			this.carrierShippingStatusId = carrierShippingStatusId;
			this.salesId = salesId;
			this.statusEntryDate = statusEntryDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SaleCarrierShippingStatus>\r\n" +
			"	<CarrierShippingStatusId>" + carrierShippingStatusId + "</CarrierShippingStatusId>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<StatusEntryDate>" + statusEntryDate + "</StatusEntryDate>\r\n" +
			"</SaleCarrierShippingStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("carrierShippingStatusId")) {
					SetXmlValue(ref carrierShippingStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("statusEntryDate")) {
					SetXmlValue(ref statusEntryDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SaleCarrierShippingStatus[] GetSaleCarrierShippingStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleCarrierShippingStatuss();
		}

		/*
		public static SaleCarrierShippingStatus GetSaleCarrierShippingStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleCarrierShippingStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSaleCarrierShippingStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSaleCarrierShippingStatus(this);
		}*/
		#endregion

		#region Properties
		public short CarrierShippingStatusId {
			set { carrierShippingStatusId = value; }
			get { return carrierShippingStatusId; }
		}

		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public DateTime StatusEntryDate {
			set { statusEntryDate = value; }
			get { return statusEntryDate; }
		}

		#endregion
	}
}
