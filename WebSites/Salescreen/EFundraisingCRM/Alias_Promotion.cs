using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AliasPromotion: EFundraisingCRMDataObject {

		private string cookieContent;
		private int promotionID;


		public AliasPromotion() : this(null) { }
		public AliasPromotion(string cookieContent) : this(cookieContent, int.MinValue) { }
		public AliasPromotion(string cookieContent, int promotionID) {
			this.cookieContent = cookieContent;
			this.promotionID = promotionID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AliasPromotion>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"</AliasPromotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AliasPromotion[] GetAliasPromotions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasPromotions();
		}

		/*
		public static AliasPromotion GetAliasPromotionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasPromotionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAliasPromotion(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAliasPromotion(this);
		}*/
		#endregion

		#region Properties
		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		#endregion
	}
}
