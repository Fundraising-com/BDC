using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductClass: EFundraisingCRMDataObject {

		private short productClassId;
		private short divisionId;
		private short accountingClassId;
		private string description;
		private string productCode;
		private string displayName;
		private int isDisplayable;
		private short minimumOrderQty;
        private bool taxExempt;


		public ProductClass() : this(short.MinValue) { }
		public ProductClass(short productClassId) : this(productClassId, short.MinValue) { }
		public ProductClass(short productClassId, short divisionId) : this(productClassId, divisionId, short.MinValue) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId) : this(productClassId, divisionId, accountingClassId, null) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId, string description) : this(productClassId, divisionId, accountingClassId, description, null) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId, string description, string productCode) : this(productClassId, divisionId, accountingClassId, description, productCode, null) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId, string description, string productCode, string displayName) : this(productClassId, divisionId, accountingClassId, description, productCode, displayName, int.MinValue) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId, string description, string productCode, string displayName, int isDisplayable) : this(productClassId, divisionId, accountingClassId, description, productCode, displayName, isDisplayable, short.MinValue) { }
		public ProductClass(short productClassId, short divisionId, short accountingClassId, string description, string productCode, string displayName, int isDisplayable, short minimumOrderQty) {
			this.productClassId = productClassId;
			this.divisionId = divisionId;
			this.accountingClassId = accountingClassId;
			this.description = description;
			this.productCode = productCode;
			this.displayName = displayName;
			this.isDisplayable = isDisplayable;
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
			"	<ProductCode>" + System.Web.HttpUtility.HtmlEncode(productCode) + "</ProductCode>\r\n" +
			"	<DisplayName>" + System.Web.HttpUtility.HtmlEncode(displayName) + "</DisplayName>\r\n" +
			"	<IsDisplayable>" + isDisplayable + "</IsDisplayable>\r\n" +
			"	<MinimumOrderQty>" + minimumOrderQty + "</MinimumOrderQty>\r\n" +
			"</ProductClass>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountingClassId")) {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productCode")) {
					SetXmlValue(ref productCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayName")) {
					SetXmlValue(ref displayName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDisplayable")) {
					SetXmlValue(ref isDisplayable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("minimumOrderQty")) {
					SetXmlValue(ref minimumOrderQty, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductClass[] GetProductClasss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductClasss();
		}

		
		public static ProductClass GetProductClassByName(string name) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductClassByName(name);
		}

		public static ProductClass GetProductClassById(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductClassById(id);
		}
		/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductClass(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductClass(this);
		}*/
		#endregion

		#region Properties
		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public short DivisionId {
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

		public string ProductCode {
			set { productCode = value; }
			get { return productCode; }
		}

		public string DisplayName {
			set { displayName = value; }
			get { return displayName; }
		}

		public int IsDisplayable {
			set { isDisplayable = value; }
			get { return isDisplayable; }
		}

		public short MinimumOrderQty {
			set { minimumOrderQty = value; }
			get { return minimumOrderQty; }
		}
       
        public bool TaxExempt
        {
            set { taxExempt = value; }
            get { return taxExempt; }
        }

		#endregion
	}
}
