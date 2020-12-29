using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ProductPriceInfo: eFundraisingStoreDataObject {

		private int productId;
		private string countryCode;
		private DateTime effectiveDate;
		private int productClassId;
		private Decimal unitPrice;


		public ProductPriceInfo() : this(int.MinValue) { }
		public ProductPriceInfo(int productId) : this(productId, null) { }
		public ProductPriceInfo(int productId, string countryCode) : this(productId, countryCode, DateTime.MinValue) { }
		public ProductPriceInfo(int productId, string countryCode, DateTime effectiveDate) : this(productId, countryCode, effectiveDate, int.MinValue) { }
		public ProductPriceInfo(int productId, string countryCode, DateTime effectiveDate, int productClassId) : this(productId, countryCode, effectiveDate, productClassId, Decimal.MinValue) { }
		public ProductPriceInfo(int productId, string countryCode, DateTime effectiveDate, int productClassId, Decimal unitPrice) {
			this.productId = productId;
			this.countryCode = countryCode;
			this.effectiveDate = effectiveDate;
			this.productClassId = productClassId;
			this.unitPrice = unitPrice;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductPriceInfo>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<EffectiveDate>" + effectiveDate + "</EffectiveDate>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<UnitPrice>" + unitPrice + "</UnitPrice>\r\n" +
			"</ProductPriceInfo>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productId") {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "effectiveDate") {
					SetXmlValue(ref effectiveDate, node.InnerText);
				}
				if(node.Name.ToLower() == "productClassId") {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "unitPrice") {
					SetXmlValue(ref unitPrice, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductPriceInfo[] GetProductPriceInfos() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPriceInfos();
		}

		public static ProductPriceInfo GetProductPriceInfoByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPriceInfoByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductPriceInfo(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductPriceInfo(this);
		}
		#endregion

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public DateTime EffectiveDate {
			set { effectiveDate = value; }
			get { return effectiveDate; }
		}

		public int ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public Decimal UnitPrice {
			set { unitPrice = value; }
			get { return unitPrice; }
		}

		#endregion
	}
}
