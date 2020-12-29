using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BrochuresImages: EFundraisingCRMDataObject {

		private short brochuresImagesId;
		private int productId;
		private string baseFilename;
		private string fileExt;
		private short numberPages;


		public BrochuresImages() : this(short.MinValue) { }
		public BrochuresImages(short brochuresImagesId) : this(brochuresImagesId, int.MinValue) { }
		public BrochuresImages(short brochuresImagesId, int productId) : this(brochuresImagesId, productId, null) { }
		public BrochuresImages(short brochuresImagesId, int productId, string baseFilename) : this(brochuresImagesId, productId, baseFilename, null) { }
		public BrochuresImages(short brochuresImagesId, int productId, string baseFilename, string fileExt) : this(brochuresImagesId, productId, baseFilename, fileExt, short.MinValue) { }
		public BrochuresImages(short brochuresImagesId, int productId, string baseFilename, string fileExt, short numberPages) {
			this.brochuresImagesId = brochuresImagesId;
			this.productId = productId;
			this.baseFilename = baseFilename;
			this.fileExt = fileExt;
			this.numberPages = numberPages;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BrochuresImages>\r\n" +
			"	<BrochuresImagesId>" + brochuresImagesId + "</BrochuresImagesId>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<BaseFilename>" + System.Web.HttpUtility.HtmlEncode(baseFilename) + "</BaseFilename>\r\n" +
			"	<FileExt>" + System.Web.HttpUtility.HtmlEncode(fileExt) + "</FileExt>\r\n" +
			"	<NumberPages>" + numberPages + "</NumberPages>\r\n" +
			"</BrochuresImages>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("brochuresImagesId")) {
					SetXmlValue(ref brochuresImagesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productId")) {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("baseFilename")) {
					SetXmlValue(ref baseFilename, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fileExt")) {
					SetXmlValue(ref fileExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("numberPages")) {
					SetXmlValue(ref numberPages, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BrochuresImages[] GetBrochuresImagess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrochuresImagess();
		}

		/*
		public static BrochuresImages GetBrochuresImagesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrochuresImagesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBrochuresImages(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBrochuresImages(this);
		}*/
		#endregion

		#region Properties
		public short BrochuresImagesId {
			set { brochuresImagesId = value; }
			get { return brochuresImagesId; }
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

		public short NumberPages {
			set { numberPages = value; }
			get { return numberPages; }
		}

		#endregion
	}
}
