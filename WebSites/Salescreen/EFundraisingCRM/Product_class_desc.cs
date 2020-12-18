using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductClassDesc: EFundraisingCRMDataObject {

		private short productClassId;
		private short languageId;
		private string productClassDesc;
		private string minRequirements;


		public ProductClassDesc() : this(short.MinValue) { }
		public ProductClassDesc(short productClassId) : this(productClassId, short.MinValue) { }
		public ProductClassDesc(short productClassId, short languageId) : this(productClassId, languageId, null) { }
		public ProductClassDesc(short productClassId, short languageId, string productClassDesc) : this(productClassId, languageId, productClassDesc, null) { }
		public ProductClassDesc(short productClassId, short languageId, string productClassDesc, string minRequirements) {
			this.productClassId = productClassId;
			this.languageId = languageId;
			this.productClassDesc = productClassDesc;
			this.minRequirements = minRequirements;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductClassDesc>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<ProductClassDesc>" + System.Web.HttpUtility.HtmlEncode(productClassDesc) + "</ProductClassDesc>\r\n" +
			"	<MinRequirements>" + System.Web.HttpUtility.HtmlEncode(minRequirements) + "</MinRequirements>\r\n" +
			"</ProductClassDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassDesc")) {
					SetXmlValue(ref productClassDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("minRequirements")) {
					SetXmlValue(ref minRequirements, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductClassDesc[] GetProductClassDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductClassDescs();
		}

		/*
		public static ProductClassDesc GetProductClassDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductClassDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductClassDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductClassDesc(this);
		}*/
		#endregion

		#region Properties
		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string ProductClassDescription {
			set { productClassDesc = value; }
			get { return productClassDesc; }
		}

		public string MinRequirements {
			set { minRequirements = value; }
			get { return minRequirements; }
		}

		#endregion
	}
}
