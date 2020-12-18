using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductsCultures: EFundraisingCRMDataObject {

		private int productId;
		private short cultureId;


		public ProductsCultures() : this(int.MinValue) { }
		public ProductsCultures(int productId) : this(productId, short.MinValue) { }
		public ProductsCultures(int productId, short cultureId) {
			this.productId = productId;
			this.cultureId = cultureId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductsCultures>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<CultureId>" + cultureId + "</CultureId>\r\n" +
			"</ProductsCultures>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productId")) {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cultureId")) {
					SetXmlValue(ref cultureId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductsCultures[] GetProductsCulturess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductsCulturess();
		}

		public static ProductsCultures GetProductsCulturesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductsCulturesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductsCultures(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductsCultures(this);
		}
		#endregion

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public short CultureId {
			set { cultureId = value; }
			get { return cultureId; }
		}

		#endregion
	}
}
