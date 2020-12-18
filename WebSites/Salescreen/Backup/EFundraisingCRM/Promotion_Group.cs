using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PromotionGroup: EFundraisingCRMDataObject {

		private int promoGroupID;
		private string description;


		public PromotionGroup() : this(int.MinValue) { }
		public PromotionGroup(int promoGroupID) : this(promoGroupID, null) { }
		public PromotionGroup(int promoGroupID, string description) {
			this.promoGroupID = promoGroupID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionGroup>\r\n" +
			"	<PromoGroupID>" + promoGroupID + "</PromoGroupID>\r\n" +
			"	<Description>" + description + "</Description>\r\n" +
			"</PromotionGroup>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promoGroupId")) {
					SetXmlValue(ref promoGroupID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionGroup[] GetPromotionGroups() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionGroups();
		}

		public static PromotionGroup GetPromotionGroupByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionGroupByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionGroup(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionGroup(this);
		}
		#endregion

		#region Properties
		public int PromoGroupID {
			set { promoGroupID = value; }
			get { return promoGroupID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
