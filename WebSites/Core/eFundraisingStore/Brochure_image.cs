using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class BrochureImage: eFundraisingStoreDataObject {

		private short brochureImageId;
		private int productId;
		private string baseFilename;
		private string fileExt;
		private short numberPage;


		public BrochureImage() : this(short.MinValue) { }
		public BrochureImage(short brochureImageId) : this(brochureImageId, int.MinValue) { }
		public BrochureImage(short brochureImageId, int productId) : this(brochureImageId, productId, null) { }
		public BrochureImage(short brochureImageId, int productId, string baseFilename) : this(brochureImageId, productId, baseFilename, null) { }
		public BrochureImage(short brochureImageId, int productId, string baseFilename, string fileExt) : this(brochureImageId, productId, baseFilename, fileExt, short.MinValue) { }
		public BrochureImage(short brochureImageId, int productId, string baseFilename, string fileExt, short numberPage) {
			this.brochureImageId = brochureImageId;
			this.productId = productId;
			this.baseFilename = baseFilename;
			this.fileExt = fileExt;
			this.numberPage = numberPage;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BrochureImage>\r\n" +
			"	<BrochureImageId>" + brochureImageId + "</BrochureImageId>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<BaseFilename>" + System.Web.HttpUtility.HtmlEncode(baseFilename) + "</BaseFilename>\r\n" +
			"	<FileExt>" + System.Web.HttpUtility.HtmlEncode(fileExt) + "</FileExt>\r\n" +
			"	<NumberPage>" + numberPage + "</NumberPage>\r\n" +
			"</BrochureImage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "brochureImageId") {
					SetXmlValue(ref brochureImageId, node.InnerText);
				}
				if(node.Name.ToLower() == "productId") {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(node.Name.ToLower() == "baseFilename") {
					SetXmlValue(ref baseFilename, node.InnerText);
				}
				if(node.Name.ToLower() == "fileExt") {
					SetXmlValue(ref fileExt, node.InnerText);
				}
				if(node.Name.ToLower() == "numberPage") {
					SetXmlValue(ref numberPage, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BrochureImage[] GetBrochureImages() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBrochureImages();
		}

		public static BrochureImage GetBrochureImageByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBrochureImageByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertBrochureImage(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateBrochureImage(this);
		}
		#endregion

		#region Properties
		public short BrochureImageId {
			set { brochureImageId = value; }
			get { return brochureImageId; }
		}

		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public string BaseFilename {
			set { baseFilename = value; }
			get { return baseFilename; }
		}

		public string FileExt {
			set { fileExt = value; }
			get { return fileExt; }
		}

		public short NumberPage {
			set { numberPage = value; }
			get { return numberPage; }
		}

		#endregion
	}
}
