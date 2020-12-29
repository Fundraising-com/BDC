using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PromotionType: eFundraisingStoreDataObject {

		private string promotionTypeCode;
		private string name;
		private DateTime createDate;


		public PromotionType() : this(null) { }
		public PromotionType(string promotionTypeCode) : this(promotionTypeCode, null) { }
		public PromotionType(string promotionTypeCode, string name) : this(promotionTypeCode, name, DateTime.MinValue) { }
		public PromotionType(string promotionTypeCode, string name, DateTime createDate) {
			this.promotionTypeCode = promotionTypeCode;
			this.name = name;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionType>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PromotionType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "promotionTypeCode") {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionType[] GetPromotionTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotionTypes();
		}
		/*
		public static PromotionType GetPromotionTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPromotionTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPromotionType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePromotionType(this);
		}*/
		#endregion

		#region Properties
		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
