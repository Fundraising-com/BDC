using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ClientActivityType: EFundraisingCRMDataObject {

		private short clientActivityTypeId;
		private short carrierShippingStatusId;
		private string description;


		public ClientActivityType() : this(short.MinValue) { }
		public ClientActivityType(short clientActivityTypeId) : this(clientActivityTypeId, short.MinValue) { }
		public ClientActivityType(short clientActivityTypeId, short carrierShippingStatusId) : this(clientActivityTypeId, carrierShippingStatusId, null) { }
		public ClientActivityType(short clientActivityTypeId, short carrierShippingStatusId, string description) {
			this.clientActivityTypeId = clientActivityTypeId;
			this.carrierShippingStatusId = carrierShippingStatusId;
			this.description = description;
		}

		#region Static Data
		
		public static ClientActivityType SOS 
		{
			get { return new ClientActivityType(1, short.MinValue, "S.O.S."); }
		}
		
		public static ClientActivityType ConfirmationCall
		{
			get { return new ClientActivityType(2, short.MinValue, "Confirmation Call"); }
		}
		
		public static ClientActivityType CallBack
		{
			get { return new ClientActivityType(3, short.MinValue, "Call Back"); }
		}
		
		public static ClientActivityType NextFundraiser
		{
			get { return new ClientActivityType(5, short.MinValue, "Next Fundraiser"); }
		}
		
		public static ClientActivityType ReferenceCall
		{
			get { return new ClientActivityType(6, short.MinValue, "Reference Call"); }
		}
		
		public static ClientActivityType PaymentCourtesyCall
		{
			get { return new ClientActivityType(7, short.MinValue, "Payment Courtesy Call"); }
		}
		
		public static ClientActivityType FirstAttemptNotIn
		{
			get { return new ClientActivityType(10, 3, "1st Attempt - Not In"); }
		}
		
		public static ClientActivityType SecondAttemptNotIn
		{
			get { return new ClientActivityType(11, 4, "2nd Attempt - Not In"); }
		}
		
		public static ClientActivityType ThirdAttemptNotIn
		{
			get { return new ClientActivityType(12, 5, "3rd Attempt - Not In"); }
		}
		
		public static ClientActivityType FirstAttemptNoFunds
		{
			get { return new ClientActivityType(13, 7, "1st Attempt - No Funds"); }
		}
		
		public static ClientActivityType SecondAttemptNoFunds
		{
			get { return new ClientActivityType(14, 8, "2nd Attempt - No Funds"); }
		}
		
		public static ClientActivityType ThirdAttemptNoFunds
		{
			get { return new ClientActivityType(15, 9, "3rd Attempt - No Funds"); }
		}
		
		public static ClientActivityType HoldForPickUp
		{
			get { return new ClientActivityType(16, 10, "Hold for Pick-Up"); }
		}
		
		public static ClientActivityType MisplaceAddress
		{
			get { return new ClientActivityType(17, 12, "Misplace Address"); }
		}
		
		public static ClientActivityType BoxReturned
		{
			get { return new ClientActivityType(18, 14, "Box Returned"); }
		}
		
		public static ClientActivityType ReceiverOnHoliday
		{
			get { return new ClientActivityType(19, 16, "Receiver On Holiday"); }
		}
		
		public static ClientActivityType BoxDamaged
		{
			get { return new ClientActivityType(20, 17, "Box Damaged"); }
		}
		
		public static ClientActivityType RefusedCOD
		{
			get { return new ClientActivityType(21, 18, "Refused COD"); }
		}
		
		public static ClientActivityType LateDelivery_Airplan_Train
		{
			get { return new ClientActivityType(22, 19, "Late Delivery: Airplan/Train"); }
		}
		
		public static ClientActivityType StillHeldaUPSDepot
		{
			get { return new ClientActivityType(23, 22, "Still Held a UPS Depot"); }
		}
		
		public static ClientActivityType Refused
		{
			get { return new ClientActivityType(24, 6, "Refused"); }
		}
		
		public static ClientActivityType InTransit
		{
			get { return new ClientActivityType(25, 2, "In Transit"); }
		}
		
		public static ClientActivityType OutforDelivery
		{
			get { return new ClientActivityType(26, 11, "Out for Delivery"); }
		}
		
		public static ClientActivityType WaitingforBoxReturn
		{
			get { return new ClientActivityType(27, 13, "Waiting for Box Return"); }
		}
		
		public static ClientActivityType ClientMove
		{
			get { return new ClientActivityType(28, 20, "Client Move"); }
		}
		
		public static ClientActivityType HoldForFutureDelivery
		{
			get { return new ClientActivityType(29, 21, "Hold For Future Delivery"); }
		}
		
		public static ClientActivityType Delivered
		{
			get { return new ClientActivityType(30, 1, "Delivered"); }
		}
		
		public static ClientActivityType CallIn
		{
			get { return new ClientActivityType(31, short.MinValue, "Call In"); }
		}
		
		public static ClientActivityType Other
		{
			get { return new ClientActivityType(32, short.MinValue, "Other"); }
		}
		
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientActivityType>\r\n" +
			"	<ClientActivityTypeId>" + clientActivityTypeId + "</ClientActivityTypeId>\r\n" +
			"	<CarrierShippingStatusId>" + carrierShippingStatusId + "</CarrierShippingStatusId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ClientActivityType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("clientActivityTypeId")) {
					SetXmlValue(ref clientActivityTypeId, node.InnerText);
				}
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
		public static ClientActivityType[] GetClientActivityTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientActivityTypes();
		}

		/*
		public static ClientActivityType GetClientActivityTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientActivityTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClientActivityType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateClientActivityType(this);
		}*/
		#endregion

		#region Properties
		public short ClientActivityTypeId {
			set { clientActivityTypeId = value; }
			get { return clientActivityTypeId; }
		}

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
