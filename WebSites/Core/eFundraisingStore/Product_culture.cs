using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ProductCulture: eFundraisingStoreDataObject {

		private int productId;
		private string cultureCode;


		public ProductCulture() : this(int.MinValue) { }
		public ProductCulture(int productId) : this(productId, null) { }
		public ProductCulture(int productId, string cultureCode) {
			this.productId = productId;
			this.cultureCode = cultureCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductCulture>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<CultureCode>" + cultureCode + "</CultureCode>\r\n" +
			"</ProductCulture>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productId") {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static ProductCulture[] GetProductCultures() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductCultures();
		}

		public static ProductCulture GetProductCultureByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductCultureByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductCulture(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductCulture(this);
		}*/
		#endregion

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		#endregion
	}
}
