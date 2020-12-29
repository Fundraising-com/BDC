using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PromotionGroupPromotion: EFundraisingCRMDataObject {

		private int promoGroupID;
		private int promotionID;


		public PromotionGroupPromotion() : this(int.MinValue) { }
		public PromotionGroupPromotion(int promoGroupID) : this(promoGroupID, int.MinValue) { }
		public PromotionGroupPromotion(int promoGroupID, int promotionID) {
			this.promoGroupID = promoGroupID;
			this.promotionID = promotionID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionGroupPromotion>\r\n" +
			"	<PromoGroupID>" + promoGroupID + "</PromoGroupID>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"</PromotionGroupPromotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promoGroupId")) {
					SetXmlValue(ref promoGroupID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionGroupPromotion[] GetPromotionGroupPromotions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionGroupPromotions();
		}

		public static PromotionGroupPromotion GetPromotionGroupPromotionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionGroupPromotionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionGroupPromotion(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionGroupPromotion(this);
		}
		#endregion

		#region Properties
		public int PromoGroupID {
			set { promoGroupID = value; }
			get { return promoGroupID; }
		}

		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		#endregion
	}
}
