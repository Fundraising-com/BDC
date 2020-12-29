using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ProductDesc: EFundraisingCRMDataObject {

		private int productDescId;
		private int productId;
		private short languageId;
		private string productName;
		private string productShortDesc;
		private string productLongDesc;
		private string productSmallImg;
		private string productLargeImg;
		private int availableOnline;


		public ProductDesc() : this(int.MinValue) { }
		public ProductDesc(int productDescId) : this(productDescId, int.MinValue) { }
		public ProductDesc(int productDescId, int productId) : this(productDescId, productId, short.MinValue) { }
		public ProductDesc(int productDescId, int productId, short languageId) : this(productDescId, productId, languageId, null) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName) : this(productDescId, productId, languageId, productName, null) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName, string productShortDesc) : this(productDescId, productId, languageId, productName, productShortDesc, null) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName, string productShortDesc, string productLongDesc) : this(productDescId, productId, languageId, productName, productShortDesc, productLongDesc, null) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName, string productShortDesc, string productLongDesc, string productSmallImg) : this(productDescId, productId, languageId, productName, productShortDesc, productLongDesc, productSmallImg, null) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName, string productShortDesc, string productLongDesc, string productSmallImg, string productLargeImg) : this(productDescId, productId, languageId, productName, productShortDesc, productLongDesc, productSmallImg, productLargeImg, int.MinValue) { }
		public ProductDesc(int productDescId, int productId, short languageId, string productName, string productShortDesc, string productLongDesc, string productSmallImg, string productLargeImg, int availableOnline) {
			this.productDescId = productDescId;
			this.productId = productId;
			this.languageId = languageId;
			this.productName = productName;
			this.productShortDesc = productShortDesc;
			this.productLongDesc = productLongDesc;
			this.productSmallImg = productSmallImg;
			this.productLargeImg = productLargeImg;
			this.availableOnline = availableOnline;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductDesc>\r\n" +
			"	<ProductDescId>" + productDescId + "</ProductDescId>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<ProductName>" + System.Web.HttpUtility.HtmlEncode(productName) + "</ProductName>\r\n" +
			"	<ProductShortDesc>" + System.Web.HttpUtility.HtmlEncode(productShortDesc) + "</ProductShortDesc>\r\n" +
			"	<ProductLongDesc>" + System.Web.HttpUtility.HtmlEncode(productLongDesc) + "</ProductLongDesc>\r\n" +
			"	<ProductSmallImg>" + System.Web.HttpUtility.HtmlEncode(productSmallImg) + "</ProductSmallImg>\r\n" +
			"	<ProductLargeImg>" + System.Web.HttpUtility.HtmlEncode(productLargeImg) + "</ProductLargeImg>\r\n" +
			"	<AvailableOnline>" + availableOnline + "</AvailableOnline>\r\n" +
			"</ProductDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productDescId")) {
					SetXmlValue(ref productDescId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productId")) {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productName")) {
					SetXmlValue(ref productName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productShortDesc")) {
					SetXmlValue(ref productShortDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productLongDesc")) {
					SetXmlValue(ref productLongDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productSmallImg")) {
					SetXmlValue(ref productSmallImg, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productLargeImg")) {
					SetXmlValue(ref productLargeImg, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("availableOnline")) {
					SetXmlValue(ref availableOnline, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductDesc[] GetProductDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductDescs();
		}

		public static ProductDesc GetProductDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductDesc(this);
		}
		#endregion

		#region Properties
		public int ProductDescId {
			set { productDescId = value; }
			get { return productDescId; }
		}

		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string ProductName {
			set { productName = value; }
			get { return productName; }
		}

		public string ProductShortDesc {
			set { productShortDesc = value; }
			get { return productShortDesc; }
		}

		public string ProductLongDesc {
			set { productLongDesc = value; }
			get { return productLongDesc; }
		}

		public string ProductSmallImg {
			set { productSmallImg = value; }
			get { return productSmallImg; }
		}

		public string ProductLargeImg {
			set { productLargeImg = value; }
			get { return productLargeImg; }
		}

		public int AvailableOnline {
			set { availableOnline = value; }
			get { return availableOnline; }
		}

		#endregion
	}
}
