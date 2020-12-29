using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ProductClassDesc: eFundraisingStoreDataObject {

		private int productClassId;
		private short languageId;
		private string productClassDesc;
		private string minRequirement;


		public ProductClassDesc() : this(int.MinValue) { }
		public ProductClassDesc(int productClassId) : this(productClassId, short.MinValue) { }
		public ProductClassDesc(int productClassId, short languageId) : this(productClassId, languageId, null) { }
		public ProductClassDesc(int productClassId, short languageId, string productClassDesc) : this(productClassId, languageId, productClassDesc, null) { }
		public ProductClassDesc(int productClassId, short languageId, string productClassDesc, string minRequirement) {
			this.productClassId = productClassId;
			this.languageId = languageId;
			this.productClassDesc = productClassDesc;
			this.minRequirement = minRequirement;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductClassDesc>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<ProductClassDesc>" + System.Web.HttpUtility.HtmlEncode(productClassDesc) + "</ProductClassDesc>\r\n" +
			"	<MinRequirement>" + System.Web.HttpUtility.HtmlEncode(minRequirement) + "</MinRequirement>\r\n" +
			"</ProductClassDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productClassId") {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "languageId") {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(node.Name.ToLower() == "productClassDesc") {
					SetXmlValue(ref productClassDesc, node.InnerText);
				}
				if(node.Name.ToLower() == "minRequirement") {
					SetXmlValue(ref minRequirement, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductClassDesc[] GetProductClassDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductClassDescs();
		}

		public static ProductClassDesc GetProductClassDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductClassDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductClassDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductClassDesc(this);
		}
		#endregion

		#region Properties
		public int ProductClassId {
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

		public string MinRequirement {
			set { minRequirement = value; }
			get { return minRequirement; }
		}

		#endregion
	}
}
