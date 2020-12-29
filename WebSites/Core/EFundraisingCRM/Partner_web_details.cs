using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PartnerWebDetails: EFundraisingCRMDataObject {

		private int partnerId;
		private string topMenu;
		private string leftMenu;
		private string rightMenu;
		private string imagesPath;
		private string defaultColor;
		private string shortCutMenu;
		private string productImageMap;


		public PartnerWebDetails() : this(int.MinValue) { }
		public PartnerWebDetails(int partnerId) : this(partnerId, null) { }
		public PartnerWebDetails(int partnerId, string topMenu) : this(partnerId, topMenu, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu) : this(partnerId, topMenu, leftMenu, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu, string rightMenu) : this(partnerId, topMenu, leftMenu, rightMenu, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu, string rightMenu, string imagesPath) : this(partnerId, topMenu, leftMenu, rightMenu, imagesPath, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu, string rightMenu, string imagesPath, string defaultColor) : this(partnerId, topMenu, leftMenu, rightMenu, imagesPath, defaultColor, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu, string rightMenu, string imagesPath, string defaultColor, string shortCutMenu) : this(partnerId, topMenu, leftMenu, rightMenu, imagesPath, defaultColor, shortCutMenu, null) { }
		public PartnerWebDetails(int partnerId, string topMenu, string leftMenu, string rightMenu, string imagesPath, string defaultColor, string shortCutMenu, string productImageMap) {
			this.partnerId = partnerId;
			this.topMenu = topMenu;
			this.leftMenu = leftMenu;
			this.rightMenu = rightMenu;
			this.imagesPath = imagesPath;
			this.defaultColor = defaultColor;
			this.shortCutMenu = shortCutMenu;
			this.productImageMap = productImageMap;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerWebDetails>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<TopMenu>" + System.Web.HttpUtility.HtmlEncode(topMenu) + "</TopMenu>\r\n" +
			"	<LeftMenu>" + System.Web.HttpUtility.HtmlEncode(leftMenu) + "</LeftMenu>\r\n" +
			"	<RightMenu>" + System.Web.HttpUtility.HtmlEncode(rightMenu) + "</RightMenu>\r\n" +
			"	<ImagesPath>" + System.Web.HttpUtility.HtmlEncode(imagesPath) + "</ImagesPath>\r\n" +
			"	<DefaultColor>" + System.Web.HttpUtility.HtmlEncode(defaultColor) + "</DefaultColor>\r\n" +
			"	<ShortCutMenu>" + System.Web.HttpUtility.HtmlEncode(shortCutMenu) + "</ShortCutMenu>\r\n" +
			"	<ProductImageMap>" + System.Web.HttpUtility.HtmlEncode(productImageMap) + "</ProductImageMap>\r\n" +
			"</PartnerWebDetails>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("topMenu")) {
					SetXmlValue(ref topMenu, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leftMenu")) {
					SetXmlValue(ref leftMenu, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("rightMenu")) {
					SetXmlValue(ref rightMenu, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("imagesPath")) {
					SetXmlValue(ref imagesPath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("defaultColor")) {
					SetXmlValue(ref defaultColor, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shortCutMenu")) {
					SetXmlValue(ref shortCutMenu, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productImageMap")) {
					SetXmlValue(ref productImageMap, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerWebDetails[] GetPartnerWebDetailss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerWebDetailss();
		}

		public static PartnerWebDetails GetPartnerWebDetailsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerWebDetailsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerWebDetails(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerWebDetails(this);
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string TopMenu {
			set { topMenu = value; }
			get { return topMenu; }
		}

		public string LeftMenu {
			set { leftMenu = value; }
			get { return leftMenu; }
		}

		public string RightMenu {
			set { rightMenu = value; }
			get { return rightMenu; }
		}

		public string ImagesPath {
			set { imagesPath = value; }
			get { return imagesPath; }
		}

		public string DefaultColor {
			set { defaultColor = value; }
			get { return defaultColor; }
		}

		public string ShortCutMenu {
			set { shortCutMenu = value; }
			get { return shortCutMenu; }
		}

		public string ProductImageMap {
			set { productImageMap = value; }
			get { return productImageMap; }
		}

		#endregion
	}
}
