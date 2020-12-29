using System;
using System.Xml;
using System.Text;


namespace GA.BDC.Core.eFundraisingStore {

	public enum PackageDescStatus
	{
		Error,
		Ok
	}

	public class PackageDesc: eFundraisingStoreDataObject {

		private int packageId;
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

        private string imageMainFolder = "2010";
       
        
   
		public PackageDesc() : this(int.MinValue) { }
		public PackageDesc(int packageId) : this(packageId, null) { }
		public PackageDesc(int packageId, string cultureCode) : this(packageId, cultureCode, int.MinValue) { }
		public PackageDesc(int packageId, string cultureCode, int templateId) : this(packageId, cultureCode, templateId, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name) : this(packageId, cultureCode, templateId, name, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc) : this(packageId, cultureCode, templateId, name, shortDesc, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, int.MinValue) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, short.MinValue) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, enabled, null) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled, string configuration) : this(packageId, cultureCode, templateId, name, shortDesc, longDesc, extraDesc, pageName, pageTitle, imageName, imageAltText, displayOrder, enabled, configuration, DateTime.MinValue) { }
		public PackageDesc(int packageId, string cultureCode, int templateId, string name, string shortDesc, string longDesc, string extraDesc, string pageName, string pageTitle, string imageName, string imageAltText, int displayOrder, short enabled, string configuration, DateTime createDate) {
			this.packageId = packageId;
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
			return "<PackageDesc>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
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
			"</PackageDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "packageId") {
					SetXmlValue(ref packageId, node.InnerText);
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
		public static PackageDesc[] GetPackageDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescs();
		}

		public static PackageDesc GetPackageDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescByID(id);
		}

		public static PackageDescCollection GetPackageDescsByImageName(string imageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescsByImageName(imageName);
		}
		public static PackageDescCollection GetChildPackageDescByPackageName(string packageName)
		{
            DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetChildPackageDescByPackageName(packageName);
		}
		public static PackageDesc GetPackageDescByPackageIDAndCultureCode(int id, string cultureCode) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescByPackageIDAndCultureCode(id, cultureCode );
		}
		public static PackageDesc GetPackageDescByPageNameAndPackageRootID(string pageName, int rootID)
		{
            DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescByPageNameAndPackageRootID(pageName, rootID);
		}
		public static PackageDesc GetPackageDescByPageNameAndTemplateExists(string pageName)
		{
            DataAccess.eFundraisingStoreDatabase dbo = new GA.BDC.Core.eFundraisingStore.DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescByPageNameAndTemplateExists(pageName);
		}
		public static PackageDescCollection GetPackageDescsByPackageIDAndPageName(int id, string pageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescsByPackageIDAndPageName(id, pageName);
		}
		
		public static PackageDescCollection GetPackageDescsByPageName(string pageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescsByPageName(pageName);
		}

		public static PackageDescCollection GetPackageDescsByPackageID(short id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageDescsByPackageID (id);
		}

		public int Insert() 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPackageDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePackageDesc(this);
		}

		#endregion

		#region Properties
		public int PackageId {
			set { packageId = value; }
			get { return packageId; }
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
			get { return name; }
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

        public string FullPathPageName
        {
            get; set;
        }


		public string PageTitle {
			set { pageTitle = value; }
			get { return pageTitle; }
		}

		public string ImageName {
			set { imageName = value; }
			get { return imageName; }
		}


        public string ImagePathBig
        {
            get { return "/UserResources/Images/Packages/" + ImageMainFolder + "/product_images/" + imageName; }
        }

        public string ImagePathThumbnailsBig
        {
            get { return "/UserResources/Images/Packages/" +ImageMainFolder + "/thumbnailsBig/" + imageName; }
        }

        public string ImagePathThumbnails
        {
            get {

                return "/UserResources/Images/Packages/" + ImageMainFolder + "/thumbnails/" + imageName;
            }
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

		public string ScratchCardImageUrl 
		{
			get { return "../../../../Resources/Images/_ScratchcardWeb_/_classic_/en-US/aboutproduct/" + ImageName; }
			
		}

		public string ScratchCardAgentImageUrl 
		{
			get { return "../../../../Resources/Images/_AgentWeb_/_classic_/en-US/aboutproduct/" + ImageName; }

        }

        #endregion

        //only use to display
        public string Profit { get; set; }
        public string ImageMainFolder { get{ return  imageMainFolder;} set{  imageMainFolder= value; } }
    
	}
}
