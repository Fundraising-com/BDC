using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerPromotion: eFundraisingStoreDataObject {

		private int partnerPromotionId;
		private int partnerId;
		private int promotionId;
		private DateTime createDate;


		public PartnerPromotion() : this(int.MinValue) { }
		public PartnerPromotion(int partnerPromotionId) : this(partnerPromotionId, int.MinValue) { }
		public PartnerPromotion(int partnerPromotionId, int partnerId) : this(partnerPromotionId, partnerId, int.MinValue) { }
		public PartnerPromotion(int partnerPromotionId, int partnerId, int promotionId) : this(partnerPromotionId, partnerId, promotionId, DateTime.MinValue) { }
		public PartnerPromotion(int partnerPromotionId, int partnerId, int promotionId, DateTime createDate) {
			this.partnerPromotionId = partnerPromotionId;
			this.partnerId = partnerId;
			this.promotionId = promotionId;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerPromotion>\r\n" +
			"	<PartnerPromotionId>" + partnerPromotionId + "</PartnerPromotionId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PartnerPromotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerPromotionId") {
					SetXmlValue(ref partnerPromotionId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "promotionId") {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerPromotion[] GetPartnerPromotions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerPromotions();
		}

		public static PartnerPromotion GetPartnerPromotionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerPromotionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerPromotion(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerPromotion(this);
		}
		#endregion

		#region Properties
		public int PartnerPromotionId {
			set { partnerPromotionId = value; }
			get { return partnerPromotionId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
