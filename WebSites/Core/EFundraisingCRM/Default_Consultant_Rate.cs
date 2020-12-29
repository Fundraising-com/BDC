using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class DefaultConsultantRate: EFundraisingCRMDataObject {

		private int consultantID;
		private string promotionTypeCode;
		private float defaultCommissionRate;


		public DefaultConsultantRate() : this(int.MinValue) { }
		public DefaultConsultantRate(int consultantID) : this(consultantID, null) { }
		public DefaultConsultantRate(int consultantID, string promotionTypeCode) : this(consultantID, promotionTypeCode, float.MinValue) { }
		public DefaultConsultantRate(int consultantID, string promotionTypeCode, float defaultCommissionRate) {
			this.consultantID = consultantID;
			this.promotionTypeCode = promotionTypeCode;
			this.defaultCommissionRate = defaultCommissionRate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<DefaultConsultantRate>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<DefaultCommissionRate>" + defaultCommissionRate + "</DefaultCommissionRate>\r\n" +
			"</DefaultConsultantRate>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("defaultCommissionRate")) {
					SetXmlValue(ref defaultCommissionRate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static DefaultConsultantRate[] GetDefaultConsultantRates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDefaultConsultantRates();
		}

		public static DefaultConsultantRate GetDefaultConsultantRateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDefaultConsultantRateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDefaultConsultantRate(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDefaultConsultantRate(this);
		}
		#endregion

		#region Properties
		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public float DefaultCommissionRate {
			set { defaultCommissionRate = value; }
			get { return defaultCommissionRate; }
		}

		#endregion
	}
}
