using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOCatalogCategory: EFundraisingCRMDataObject {

		private int catalogCategoryID;
		private string description;


		public EFOCatalogCategory() : this(int.MinValue) { }
		public EFOCatalogCategory(int catalogCategoryID) : this(catalogCategoryID, null) { }
		public EFOCatalogCategory(int catalogCategoryID, string description) {
			this.catalogCategoryID = catalogCategoryID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOCatalogCategory>\r\n" +
			"	<CatalogCategoryID>" + catalogCategoryID + "</CatalogCategoryID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</EFOCatalogCategory>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("catalogCategoryId")) {
					SetXmlValue(ref catalogCategoryID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOCatalogCategory[] GetEFOCatalogCategorys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCatalogCategorys();
		}

		public static EFOCatalogCategory GetEFOCatalogCategoryByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCatalogCategoryByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOCatalogCategory(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOCatalogCategory(this);
		}
		#endregion

		#region Properties
		public int CatalogCategoryID {
			set { catalogCategoryID = value; }
			get { return catalogCategoryID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
