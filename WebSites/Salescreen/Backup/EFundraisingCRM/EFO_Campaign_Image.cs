using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOCampaignImage: EFundraisingCRMDataObject {

		private int campaignImageID;
		private string imageCatalogPath;
		private string imageCatalogPathSel;
		private int catalogCategoryID;
		private int isPersonalized;


		public EFOCampaignImage() : this(int.MinValue) { }
		public EFOCampaignImage(int campaignImageID) : this(campaignImageID, null) { }
		public EFOCampaignImage(int campaignImageID, string imageCatalogPath) : this(campaignImageID, imageCatalogPath, null) { }
		public EFOCampaignImage(int campaignImageID, string imageCatalogPath, string imageCatalogPathSel) : this(campaignImageID, imageCatalogPath, imageCatalogPathSel, int.MinValue) { }
		public EFOCampaignImage(int campaignImageID, string imageCatalogPath, string imageCatalogPathSel, int catalogCategoryID) : this(campaignImageID, imageCatalogPath, imageCatalogPathSel, catalogCategoryID, int.MinValue) { }
		public EFOCampaignImage(int campaignImageID, string imageCatalogPath, string imageCatalogPathSel, int catalogCategoryID, int isPersonalized) {
			this.campaignImageID = campaignImageID;
			this.imageCatalogPath = imageCatalogPath;
			this.imageCatalogPathSel = imageCatalogPathSel;
			this.catalogCategoryID = catalogCategoryID;
			this.isPersonalized = isPersonalized;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOCampaignImage>\r\n" +
			"	<CampaignImageID>" + campaignImageID + "</CampaignImageID>\r\n" +
			"	<ImageCatalogPath>" + System.Web.HttpUtility.HtmlEncode(imageCatalogPath) + "</ImageCatalogPath>\r\n" +
			"	<ImageCatalogPathSel>" + System.Web.HttpUtility.HtmlEncode(imageCatalogPathSel) + "</ImageCatalogPathSel>\r\n" +
			"	<CatalogCategoryID>" + catalogCategoryID + "</CatalogCategoryID>\r\n" +
			"	<IsPersonalized>" + isPersonalized + "</IsPersonalized>\r\n" +
			"</EFOCampaignImage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("campaignImageId")) {
					SetXmlValue(ref campaignImageID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("imageCatalogPath")) {
					SetXmlValue(ref imageCatalogPath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("imageCatalogPathSel")) {
					SetXmlValue(ref imageCatalogPathSel, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("catalogCategoryId")) {
					SetXmlValue(ref catalogCategoryID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isPersonalized")) {
					SetXmlValue(ref isPersonalized, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOCampaignImage[] GetEFOCampaignImages() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaignImages();
		}

		public static EFOCampaignImage GetEFOCampaignImageByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaignImageByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOCampaignImage(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOCampaignImage(this);
		}
		#endregion

		#region Properties
		public int CampaignImageID {
			set { campaignImageID = value; }
			get { return campaignImageID; }
		}

		public string ImageCatalogPath {
			set { imageCatalogPath = value; }
			get { return imageCatalogPath; }
		}

		public string ImageCatalogPathSel {
			set { imageCatalogPathSel = value; }
			get { return imageCatalogPathSel; }
		}

		public int CatalogCategoryID {
			set { catalogCategoryID = value; }
			get { return catalogCategoryID; }
		}

		public int IsPersonalized {
			set { isPersonalized = value; }
			get { return isPersonalized; }
		}

		#endregion
	}
}
