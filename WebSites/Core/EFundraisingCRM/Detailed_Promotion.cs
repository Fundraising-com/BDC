using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class DetailedPromotion: EFundraisingCRMDataObject {

		private int promotionID;
		private string promotionTypeCode;
		private string targetAgeGroupCode;
		private string targetGenderGroupCode;
		private string targetGroupCode;
		private int promotionYear;
		private int promotionMonth;
		private string description;
		private int quantitySent;
		private int callGoal;
		private int cardBudget;


		public DetailedPromotion() : this(int.MinValue) { }
		public DetailedPromotion(int promotionID) : this(promotionID, null) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode) : this(promotionID, promotionTypeCode, null) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode) : this(promotionID, promotionTypeCode, targetAgeGroupCode, null) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, null) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, int.MinValue) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, promotionYear, int.MinValue) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear, int promotionMonth) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, promotionYear, promotionMonth, null) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear, int promotionMonth, string description) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, promotionYear, promotionMonth, description, int.MinValue) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear, int promotionMonth, string description, int quantitySent) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, promotionYear, promotionMonth, description, quantitySent, int.MinValue) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear, int promotionMonth, string description, int quantitySent, int callGoal) : this(promotionID, promotionTypeCode, targetAgeGroupCode, targetGenderGroupCode, targetGroupCode, promotionYear, promotionMonth, description, quantitySent, callGoal, int.MinValue) { }
		public DetailedPromotion(int promotionID, string promotionTypeCode, string targetAgeGroupCode, string targetGenderGroupCode, string targetGroupCode, int promotionYear, int promotionMonth, string description, int quantitySent, int callGoal, int cardBudget) {
			this.promotionID = promotionID;
			this.promotionTypeCode = promotionTypeCode;
			this.targetAgeGroupCode = targetAgeGroupCode;
			this.targetGenderGroupCode = targetGenderGroupCode;
			this.targetGroupCode = targetGroupCode;
			this.promotionYear = promotionYear;
			this.promotionMonth = promotionMonth;
			this.description = description;
			this.quantitySent = quantitySent;
			this.callGoal = callGoal;
			this.cardBudget = cardBudget;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<DetailedPromotion>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<TargetAgeGroupCode>" + System.Web.HttpUtility.HtmlEncode(targetAgeGroupCode) + "</TargetAgeGroupCode>\r\n" +
			"	<TargetGenderGroupCode>" + System.Web.HttpUtility.HtmlEncode(targetGenderGroupCode) + "</TargetGenderGroupCode>\r\n" +
			"	<TargetGroupCode>" + System.Web.HttpUtility.HtmlEncode(targetGroupCode) + "</TargetGroupCode>\r\n" +
			"	<PromotionYear>" + promotionYear + "</PromotionYear>\r\n" +
			"	<PromotionMonth>" + promotionMonth + "</PromotionMonth>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<QuantitySent>" + quantitySent + "</QuantitySent>\r\n" +
			"	<CallGoal>" + callGoal + "</CallGoal>\r\n" +
			"	<CardBudget>" + cardBudget + "</CardBudget>\r\n" +
			"</DetailedPromotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetAgeGroupCode")) {
					SetXmlValue(ref targetAgeGroupCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetGenderGroupCode")) {
					SetXmlValue(ref targetGenderGroupCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetGroupCode")) {
					SetXmlValue(ref targetGroupCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionYear")) {
					SetXmlValue(ref promotionYear, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionMonth")) {
					SetXmlValue(ref promotionMonth, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantitySent")) {
					SetXmlValue(ref quantitySent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("callGoal")) {
					SetXmlValue(ref callGoal, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cardBudget")) {
					SetXmlValue(ref cardBudget, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static DetailedPromotion[] GetDetailedPromotions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDetailedPromotions();
		}

		public static DetailedPromotion GetDetailedPromotionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDetailedPromotionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDetailedPromotion(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDetailedPromotion(this);
		}
		#endregion

		#region Properties
		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public string TargetAgeGroupCode {
			set { targetAgeGroupCode = value; }
			get { return targetAgeGroupCode; }
		}

		public string TargetGenderGroupCode {
			set { targetGenderGroupCode = value; }
			get { return targetGenderGroupCode; }
		}

		public string TargetGroupCode {
			set { targetGroupCode = value; }
			get { return targetGroupCode; }
		}

		public int PromotionYear {
			set { promotionYear = value; }
			get { return promotionYear; }
		}

		public int PromotionMonth {
			set { promotionMonth = value; }
			get { return promotionMonth; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int QuantitySent {
			set { quantitySent = value; }
			get { return quantitySent; }
		}

		public int CallGoal {
			set { callGoal = value; }
			get { return callGoal; }
		}

		public int CardBudget {
			set { cardBudget = value; }
			get { return cardBudget; }
		}

		#endregion
	}
}
