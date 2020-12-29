using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadPromotion: EFundraisingCRMDataObject {

		private int leadPromotionId;
		private int leadId;
		private int promotionId;
		private DateTime entryDate;


		public LeadPromotion() : this(int.MinValue) { }
		public LeadPromotion(int leadPromotionId) : this(leadPromotionId, int.MinValue) { }
		public LeadPromotion(int leadPromotionId, int leadId) : this(leadPromotionId, leadId, int.MinValue) { }
		public LeadPromotion(int leadPromotionId, int leadId, int promotionId) : this(leadPromotionId, leadId, promotionId, DateTime.MinValue) { }
		public LeadPromotion(int leadPromotionId, int leadId, int promotionId, DateTime entryDate) {
			this.leadPromotionId = leadPromotionId;
			this.leadId = leadId;
			this.promotionId = promotionId;
			this.entryDate = entryDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadPromotion>\r\n" +
			"	<LeadPromotionId>" + leadPromotionId + "</LeadPromotionId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<EntryDate>" + entryDate + "</EntryDate>\r\n" +
			"</LeadPromotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadPromotionId")) {
					SetXmlValue(ref leadPromotionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryDate")) {
					SetXmlValue(ref entryDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadPromotion[] GetLeadPromotions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPromotions();
		}

		public static LeadPromotion GetLeadPromotionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPromotionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadPromotion(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadPromotion(this);
		}
		#endregion

		#region Properties
		public int LeadPromotionId {
			set { leadPromotionId = value; }
			get { return leadPromotionId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public DateTime EntryDate {
			set { entryDate = value; }
			get { return entryDate; }
		}

		#endregion
	}
}
