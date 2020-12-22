using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {




	public class CarrierShippingOption: EFundraisingCRMDataObject 
	{

		private short shippingOptionId;
		private string description;


		public CarrierShippingOption() : this(short.MinValue) { }
		public CarrierShippingOption(short shippingOptionId) : this(shippingOptionId, null) { }
		public CarrierShippingOption(short shippingOptionId, string description) {
			this.shippingOptionId = shippingOptionId;
			this.description = description;
		}

		#region Static Data
		public static CarrierShippingOption GROUND {
			get { return new CarrierShippingOption(1, "GROUND"); }
		}

		public static CarrierShippingOption NextDayAirSaver {
			get { return new CarrierShippingOption(2, "NEXT DAY AIR SAVER"); }
		}

		public static CarrierShippingOption ThreeDaysSelect {
			get { return new CarrierShippingOption(3, "3 DAYS SELECT"); }
		}

		public static CarrierShippingOption SecondDayAir {
			get { return new CarrierShippingOption(4, "SECOND DAY AIR"); }
		}

		public static CarrierShippingOption NextDayAir {
			get { return new CarrierShippingOption(5, "NEXT DAY AIR"); }
		}

		public static CarrierShippingOption NextDayEarlyAM {
			get { return new CarrierShippingOption(6, "NEXT DAY EARLY AM"); }
		}

		public static CarrierShippingOption Air {
			get { return new CarrierShippingOption(7, "AIR"); }
		}

		public static CarrierShippingOption NoOption {
			get { return new CarrierShippingOption(8, "NO OPTION"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CarrierShippingOption>\r\n" +
			"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CarrierShippingOption>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("shippingOptionId")) {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CarrierShippingOption[] GetCarrierShippingOptions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarrierShippingOptions();
		}

		/*
		public static CarrierShippingOption GetCarrierShippingOptionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCarrierShippingOptionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCarrierShippingOption(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCarrierShippingOption(this);
		}*/
		#endregion

		#region Properties
		public short ShippingOptionId {
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
