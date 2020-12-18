using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PromotionCost: EFundraisingCRMDataObject {

		private int promotionID;
		private int periodMonth;
		private int periodYear;
		private float cost;


		public PromotionCost() : this(int.MinValue) { }
		public PromotionCost(int promotionID) : this(promotionID, int.MinValue) { }
		public PromotionCost(int promotionID, int periodMonth) : this(promotionID, periodMonth, int.MinValue) { }
		public PromotionCost(int promotionID, int periodMonth, int periodYear) : this(promotionID, periodMonth, periodYear, float.MinValue) { }
		public PromotionCost(int promotionID, int periodMonth, int periodYear, float cost) {
			this.promotionID = promotionID;
			this.periodMonth = periodMonth;
			this.periodYear = periodYear;
			this.cost = cost;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionCost>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"	<PeriodMonth>" + periodMonth + "</PeriodMonth>\r\n" +
			"	<PeriodYear>" + periodYear + "</PeriodYear>\r\n" +
			"	<Cost>" + cost + "</Cost>\r\n" +
			"</PromotionCost>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("periodMonth")) {
					SetXmlValue(ref periodMonth, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("periodYear")) {
					SetXmlValue(ref periodYear, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cost")) {
					SetXmlValue(ref cost, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionCost[] GetPromotionCosts() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionCosts();
		}

		public static PromotionCost GetPromotionCostByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionCostByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionCost(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionCost(this);
		}
		#endregion

		#region Properties
		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public int PeriodMonth {
			set { periodMonth = value; }
			get { return periodMonth; }
		}

		public int PeriodYear {
			set { periodYear = value; }
			get { return periodYear; }
		}

		public float Cost {
			set { cost = value; }
			get { return cost; }
		}

		#endregion
	}
}
