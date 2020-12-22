using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PromotionCode: EFundraisingCRMDataObject {

		private int promotionCodeID;
		private string promotionCodeDesc;


		public PromotionCode() : this(int.MinValue) { }
		public PromotionCode(int promotionCodeID) : this(promotionCodeID, null) { }
		public PromotionCode(int promotionCodeID, string promotionCodeDesc) {
			this.promotionCodeID = promotionCodeID;
			this.promotionCodeDesc = promotionCodeDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionCode>\r\n" +
			"	<PromotionCodeID>" + promotionCodeID + "</PromotionCodeID>\r\n" +
			"	<PromotionCodeDesc>" + System.Web.HttpUtility.HtmlEncode(promotionCodeDesc) + "</PromotionCodeDesc>\r\n" +
			"</PromotionCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionCodeId")) {
					SetXmlValue(ref promotionCodeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionCodeDesc")) {
					SetXmlValue(ref promotionCodeDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionCode[] GetPromotionCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionCodes();
		}

		public static PromotionCode GetPromotionCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionCode(this);
		}
		#endregion

		#region Properties
		public int PromotionCodeID {
			set { promotionCodeID = value; }
			get { return promotionCodeID; }
		}

		public string PromotionCodeDesc {
			set { promotionCodeDesc = value; }
			get { return promotionCodeDesc; }
		}

		#endregion
	}
}
