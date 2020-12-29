using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ProductClass: eFundraisingStoreDataObject {

		private int productClassId;
		private int divisionId;
		private short accountingClassId;
		private string description;
		private short display;
		private short minimumOrderQty;


		public ProductClass() : this(int.MinValue) { }
		public ProductClass(int productClassId) : this(productClassId, int.MinValue) { }
		public ProductClass(int productClassId, int divisionId) : this(productClassId, divisionId, short.MinValue) { }
		public ProductClass(int productClassId, int divisionId, short accountingClassId) : this(productClassId, divisionId, accountingClassId, null) { }
		public ProductClass(int productClassId, int divisionId, short accountingClassId, string description) : this(productClassId, divisionId, accountingClassId, description, short.MinValue) { }
		public ProductClass(int productClassId, int divisionId, short accountingClassId, string description, short display) : this(productClassId, divisionId, accountingClassId, description, display, short.MinValue) { }
		public ProductClass(int productClassId, int divisionId, short accountingClassId, string description, short display, short minimumOrderQty) {
			this.productClassId = productClassId;
			this.divisionId = divisionId;
			this.accountingClassId = accountingClassId;
			this.description = description;
			this.display = display;
			this.minimumOrderQty = minimumOrderQty;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductClass>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Display>" + display + "</Display>\r\n" +
			"	<MinimumOrderQty>" + minimumOrderQty + "</MinimumOrderQty>\r\n" +
			"</ProductClass>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productClassId") {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "divisionId") {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(node.Name.ToLower() == "accountingClassId") {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "display") {
					SetXmlValue(ref display, node.InnerText);
				}
				if(node.Name.ToLower() == "minimumOrderQty") {
					SetXmlValue(ref minimumOrderQty, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductClass[] GetProductClasss() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductClasss();
		}

		public static ProductClass GetProductClassByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductClassByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductClass(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductClass(this);
		}
		#endregion

		#region Properties
		public int ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public int DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public short Display {
			set { display = value; }
			get { return display; }
		}

		public short MinimumOrderQty {
			set { minimumOrderQty = value; }
			get { return minimumOrderQty; }
		}

		#endregion
	}
}
