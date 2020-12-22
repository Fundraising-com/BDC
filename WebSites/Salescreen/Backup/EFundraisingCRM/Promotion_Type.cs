using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PromotionType: EFundraisingCRMDataObject {

		private string promotionTypeCode;
		private string description;
		private float defaultCommissionRate;
		private int channel;


		public PromotionType() : this(null) { }
		public PromotionType(string promotionTypeCode) : this(promotionTypeCode, null) { }
		public PromotionType(string promotionTypeCode, string description) : this(promotionTypeCode, description, float.MinValue) { }
		public PromotionType(string promotionTypeCode, string description, float defaultCommissionRate) : this(promotionTypeCode, description, defaultCommissionRate, int.MinValue) { }
		public PromotionType(string promotionTypeCode, string description, float defaultCommissionRate, int channel) {
			this.promotionTypeCode = promotionTypeCode;
			this.description = description;
			this.defaultCommissionRate = defaultCommissionRate;
			this.channel = channel;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionType>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<DefaultCommissionRate>" + defaultCommissionRate + "</DefaultCommissionRate>\r\n" +
			"	<Channel>" + channel + "</Channel>\r\n" +
			"</PromotionType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("defaultCommissionRate")) {
					SetXmlValue(ref defaultCommissionRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channel")) {
					SetXmlValue(ref channel, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionType[] GetPromotionTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionTypes();
		}

		/*
		public static PromotionType GetPromotionTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionType(this);
		}*/
		#endregion

		#region Properties
		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public float DefaultCommissionRate {
			set { defaultCommissionRate = value; }
			get { return defaultCommissionRate; }
		}

		public int Channel {
			set { channel = value; }
			get { return channel; }
		}

		#endregion
	}
}
