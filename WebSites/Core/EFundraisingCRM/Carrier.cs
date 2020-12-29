using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Carrier: EFundraisingCRMDataObject {

		private short carrierId;
		private string description;
        private bool active; 


		public Carrier() : this(short.MinValue) { }
		public Carrier(short carrierId) : this(carrierId, null) { }
		public Carrier(short carrierId, string description) {
			this.carrierId = carrierId;
			this.description = description;
		}

		#region Static Data
		public static Carrier UPS {
			get { return new Carrier(1, "UPS"); }
		}

		public static Carrier FEDEX {
			get { return new Carrier(2, "FEDEX"); }
		}

		public static Carrier PUROLATOR {
			get { return new Carrier(3, "PUROLATOR"); }
		}

		public static Carrier CAN_PAR {
			get { return new Carrier(4, "CAN PAR"); }
		}

		public static Carrier BAX_GLOBAL {
			get { return new Carrier(5, "BAX GLOBAL"); }
		}

		public static Carrier DELIVERY_POINT {
			get { return new Carrier(6, "DELIVERY-POINT"); }
		}

		public static Carrier PICKUP_AT_WAREHOUSE {
			get { return new Carrier(7, "PICKUP AT WAREHOUSE"); }
		}

		public static Carrier PROAIR {
			get { return new Carrier(8, "PROAIR"); }
		}

		public static Carrier EDS {
			get { return new Carrier(9, "EDS"); }
		}

		public static Carrier RegularMail {
			get { return new Carrier(10, "REGULAR MAIL"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Carrier>\r\n" +
			"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</Carrier>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("carrierId")) {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Carrier[] GetCarriers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarriers();
		}

		public static Carrier GetCarrierByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarrierByID(id);
		}
		/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCarrier(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCarrier(this);
		}*/
		#endregion

		#region Properties
		public short CarrierId {
			set { carrierId = value; }
			get { return carrierId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public bool Active
		{
			set { active = value; }
			get { return active; }
		}

		#endregion

	}
}
