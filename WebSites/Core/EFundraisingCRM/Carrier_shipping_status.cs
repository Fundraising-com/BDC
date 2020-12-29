using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CarrierShippingStatus: EFundraisingCRMDataObject {

		private short carrierShippingStatusId;
		private string description;


		public CarrierShippingStatus() : this(short.MinValue) { }
		public CarrierShippingStatus(short carrierShippingStatusId) : this(carrierShippingStatusId, null) { }
		public CarrierShippingStatus(short carrierShippingStatusId, string description) {
			this.carrierShippingStatusId = carrierShippingStatusId;
			this.description = description;
		}

		#region Static Data
		public static CarrierShippingStatus Delivered {
			get { return new CarrierShippingStatus(1, "Delivered"); }
		}
		public static CarrierShippingStatus InTransit {
			get { return new CarrierShippingStatus(2, "In Transit"); }
		}
		public static CarrierShippingStatus FirstAttempt_NotIn {
			get { return new CarrierShippingStatus(3, "1st Attempt - Not In"); }
		}
		public static CarrierShippingStatus SecondAttempt_NotIn {
			get { return new CarrierShippingStatus(4, "2nd Attempt - Not In"); }
		}
		public static CarrierShippingStatus ThridAttempt_NotIn {
			get { return new CarrierShippingStatus(5, "3rd Attempt - Not In"); }
		}
		public static CarrierShippingStatus Refused {
			get { return new CarrierShippingStatus(6, "Refused"); }
		}
		public static CarrierShippingStatus FirstAttempt_NoFunds {
			get { return new CarrierShippingStatus(7, "1st Attempt - No Funds"); }
		}
		public static CarrierShippingStatus SecondAttempt_NoFunds {
			get { return new CarrierShippingStatus(8, "2nd Attempt - No Funds"); }
		}
		public static CarrierShippingStatus ThirdAttempt_NoFunds {
			get { return new CarrierShippingStatus(9, "3rd Attempt - No Funds"); }
		}
		public static CarrierShippingStatus HoldForPickUp {
			get { return new CarrierShippingStatus(10, "Hold For Pick-Up"); }
		}
		public static CarrierShippingStatus OutforDelivery {
			get { return new CarrierShippingStatus(11, "Out for Delivery"); }
		}
		public static CarrierShippingStatus MisplaceAddress {
			get { return new CarrierShippingStatus(12, "Misplace Address"); }
		}
		public static CarrierShippingStatus WaitingForBoxReturn {
			get { return new CarrierShippingStatus(13, "Waiting for box return"); }
		}
		public static CarrierShippingStatus BoxReturned {
			get { return new CarrierShippingStatus(14, "Box returned"); }
		}
		public static CarrierShippingStatus Credited {
			get { return new CarrierShippingStatus(15, "Credited"); }
		}
		public static CarrierShippingStatus ReceiverOnHoliday {
			get { return new CarrierShippingStatus(16, "Receiver On Holiday"); }
		}
		public static CarrierShippingStatus BoxDamaged {
			get { return new CarrierShippingStatus(17, "Box Damaged"); }
		}
		public static CarrierShippingStatus RefusedCOD {
			get { return new CarrierShippingStatus(18, "Refused COD"); }
		}
		public static CarrierShippingStatus LateDelivery_Airplan_Train {
			get { return new CarrierShippingStatus(19, "Late Delivery: Airplan/Train"); }
		}
		public static CarrierShippingStatus ClientMoved {
			get { return new CarrierShippingStatus(20, "Client Move"); }
		}
		public static CarrierShippingStatus HoldForFutureDelivery {
			get { return new CarrierShippingStatus(21, "Hold For Future Delivery"); }
		}
		public static CarrierShippingStatus StillHeldUPSDepot {
			get { return new CarrierShippingStatus(22, "Still held a UPS Depot"); }
		}
		public static CarrierShippingStatus Exception {
			get { return new CarrierShippingStatus(24, "Exception"); }
		}
		public static CarrierShippingStatus NotAvailable {
			get { return new CarrierShippingStatus(29, "N/A"); }
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CarrierShippingStatus>\r\n" +
				"	<CarrierShippingStatusId>" + carrierShippingStatusId + "</CarrierShippingStatusId>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"</CarrierShippingStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("carrierShippingStatusId")) {
					SetXmlValue(ref carrierShippingStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CarrierShippingStatus[] GetCarrierShippingStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarrierShippingStatuss();
		}

		/*
		public static CarrierShippingStatus GetCarrierShippingStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarrierShippingStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCarrierShippingStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCarrierShippingStatus(this);
		}*/
		#endregion

		#region Properties
		public short CarrierShippingStatusId {
			set { carrierShippingStatusId = value; }
			get { return carrierShippingStatusId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
