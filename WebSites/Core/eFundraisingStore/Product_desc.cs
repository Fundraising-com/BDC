using System;
using System.Security.AccessControl;
using System.Xml;
using System.Text;

namespace GA.BDC.Core.eFundraisingStore {

	public enum ProductDescStatus
	{
		Error,
		Ok
	}

	public class ProductDesc: eFundraisingStoreDataObject {

		private int productId;
		private string cultureCode;
		private int templateId;
		private string name;
		private string shortDesc;
		private string longDesc;
		private string extraDesc;
		private string pageName;
		private string pageTitle;
		private string imageName;
		private string imageAltText;
		private int displayOrder;
		private short enabled;
		private string configuration;
		private DateTime createDate;
        private XmlDocument xmldoc;

        private string profit;

		public ProductDesc() : this(int.MinValue) { }
		public ProductDesc(int productId) : this(productId, null) { }
		public ProductDesc(int productId, string cultureCode) : this(productId, cultureCode, int.MinValue) { }
		public ProductDesc(int productId, string cultureCode, int templateId) : this(productId, cultureCode, templateId, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name) : this(productId, cultureCode, templateId, name, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc) : this(productId, cultureCode, templateId, name, shortDesc, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, int.MinValue) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, short.MinValue) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, enabled, null) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled, string configuration) : this(productId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, enabled, configuration, DateTime.MinValue) { }
		public ProductDesc(int productId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled, string configuration, DateTime createDate) {
			this.productId = productId;
			this.cultureCode = cultureCode;
			this.templateId = templateId;
			this.name = name;
			this.shortDesc = shortDesc;
			this.longDesc = longDesc;
			this.extraDesc = extraDesc;
			this.pageName = pageName;
			this.pageTitle = pageTitle;
			this.imageName = imageName;
			this.imageAltText = imageAltText;
			this.displayOrder = displayOrder;
			this.enabled = enabled;
			this.configuration = configuration;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductDesc>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<TemplateId>" + templateId + "</TemplateId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<ShortDesc>" + System.Web.HttpUtility.HtmlEncode(shortDesc) + "</ShortDesc>\r\n" +
			"	<LongDesc>" + System.Web.HttpUtility.HtmlEncode(longDesc) + "</LongDesc>\r\n" +
			"	<ExtraDesc>" + System.Web.HttpUtility.HtmlEncode(extraDesc) + "</ExtraDesc>\r\n" +
			"	<PageName>" + System.Web.HttpUtility.HtmlEncode(pageName) + "</PageName>\r\n" +
			"	<PageTitle>" + System.Web.HttpUtility.HtmlEncode(pageTitle) + "</PageTitle>\r\n" +
			"	<ImageName>" + System.Web.HttpUtility.HtmlEncode(imageName) + "</ImageName>\r\n" +
			"	<ImageAltText>" + System.Web.HttpUtility.HtmlEncode(imageAltText) + "</ImageAltText>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Enabled>" + enabled + "</Enabled>\r\n" +
			"	<Configuration>" + System.Web.HttpUtility.HtmlEncode(configuration) + "</Configuration>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</ProductDesc>\r\n";
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
				if(node.Name.ToLower() == "templateId") {
					SetXmlValue(ref templateId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "shortDesc") {
					SetXmlValue(ref shortDesc, node.InnerText);
				}
				if(node.Name.ToLower() == "longDesc") {
					SetXmlValue(ref longDesc, node.InnerText);
				}
				if(node.Name.ToLower() == "extraDesc") {
					SetXmlValue(ref extraDesc, node.InnerText);
				}
				if(node.Name.ToLower() == "pageName") {
					SetXmlValue(ref pageName, node.InnerText);
				}
				if(node.Name.ToLower() == "pageTitle") {
					SetXmlValue(ref pageTitle, node.InnerText);
				}
				if(node.Name.ToLower() == "imageName") {
					SetXmlValue(ref imageName, node.InnerText);
				}
				if(node.Name.ToLower() == "imageAltText") {
					SetXmlValue(ref imageAltText, node.InnerText);
				}
				if(node.Name.ToLower() == "displayOrder") {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(node.Name.ToLower() == "enabled") {
					SetXmlValue(ref enabled, node.InnerText);
				}
				if(node.Name.ToLower() == "configuration") {
					SetXmlValue(ref configuration, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductDesc[] GetProductDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescs();
		}

		public static ProductDesc GetProductDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescByID(id);
		}
		
		public static ProductDesc GetProductDescByPageNameAndPackageRootID(string name)
		{
			DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescByPageNameAndPackageRootID(name);
		}
		
		public static ProductDescCollection GetProductDescsByPageName(string name)
		{
            DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescsByPageName(name);
		}
				
		public static ProductDesc GetProductDescByPageNameAndTemplateExists(string name)
		{
            DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescByPageNameAndTemplateExists(name);
		}		
		public static ProductDescCollection GetProductDescsByImageName(string imageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescsByImageName(imageName);
		}

		public static ProductDescCollection GetProductDescsByProductID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescsByProductID (id);
		}

		public static ProductDesc GetProductDescByProductIDAndCultureCode(int id, string cultureCode) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductDescByProductIDAndCultureCode(id, cultureCode );
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductDesc(this);
		}
		


        public string Profit
        {
            set { profit = value; }
            get { return profit; }
        }

		#endregion

        private void affecXmlElement(out string property, string value)
        {
            property = "";
            XmlNode node = xmldoc.DocumentElement.SelectSingleNode(value);
            if (node != null)
            {
                property = node.InnerXml;
            }
        }

        public void loadXmlValue()
        {
            fetchXml(ExtraDesc);
        }

        private void fetchXml(string input)
        {
            try
            {
                input = input.Trim();
                if (input != "" && input != null && input != "-")
                {
                    StringBuilder picUrl = new StringBuilder();
                    xmldoc = new XmlDocument();
                    xmldoc.LoadXml(input);

                    affecXmlElement(out this.profit, "Profit");

                    /*
                    affecXmlElement(out this.profitChartFilename, "ProfitChart");
                    
                    affecXmlElement(out this.ProfitLine, "ProfitLine");

                    affecXmlElement(out this.PricePerItem, "PricePerItem");
                    affecXmlElement(out this.Desc, "Desc");
                    affecXmlElement(out this.MinimumOrder, "MinimumOrder");
                    affecXmlElement(out this.MinimumDay, "MinimumDay");
                    affecXmlElement(out this.FundraiserDetails, "FundraiserDetails");
                    affecXmlElement(out this.Wesuggested, "WeSuggested");
                     */
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public int TemplateId {
			set { templateId = value; }
			get { return templateId; }
		}

		public string Name {
			set { name = value; }
			get {
					return name;
				
			}
		}

		public string ShortDesc {
			set { shortDesc = value; }
			get { return shortDesc; }
		}

		public string LongDesc {
			set { longDesc = value; }
			get { return longDesc; }
		}

		public string ExtraDesc {
			set { extraDesc = value; }
			get { return extraDesc; }
		}

		public string PageName {
			set { pageName = value; }
			get { return pageName; }
		}

		public string PageTitle {
			set { pageTitle = value; }
			get { return pageTitle; }
		}

		public string ImageName {
			set { imageName = value; }
			get { return imageName; }
		}

		public string ImageAltText {
			set { imageAltText = value; }
			get { return imageAltText; }
		}

		public int DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		public short Enabled {
			set { enabled = value; }
			get { return enabled; }
		}

		public string Configuration {
			set { configuration = value; }
			get { return configuration; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
