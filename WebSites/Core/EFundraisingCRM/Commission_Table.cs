using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CommissionTable: EFundraisingCRMDataObject {

		private string promotionTypeCode;
		private string channelCode;
		private float commissionRate;


		public CommissionTable() : this(null) { }
		public CommissionTable(string promotionTypeCode) : this(promotionTypeCode, null) { }
		public CommissionTable(string promotionTypeCode, string channelCode) : this(promotionTypeCode, channelCode, float.MinValue) { }
		public CommissionTable(string promotionTypeCode, string channelCode, float commissionRate) {
			this.promotionTypeCode = promotionTypeCode;
			this.channelCode = channelCode;
			this.commissionRate = commissionRate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CommissionTable>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<CommissionRate>" + commissionRate + "</CommissionRate>\r\n" +
			"</CommissionTable>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channelCode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRate")) {
					SetXmlValue(ref commissionRate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CommissionTable[] GetCommissionTables() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionTables();
		}

		/*
		public static CommissionTable GetCommissionTableByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionTableByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionTable(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionTable(this);
		}*/
		#endregion

		#region Properties
		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		public float CommissionRate {
			set { commissionRate = value; }
			get { return commissionRate; }
		}

		#endregion
	}
}
