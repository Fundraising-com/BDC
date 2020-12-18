using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductQuantity: EFundraisingCRMDataObject {

		private int productQuantityID;
		private int scratchBookID;
		private int quantity;
		private string comments;


		public ProductQuantity() : this(int.MinValue) { }
		public ProductQuantity(int productQuantityID) : this(productQuantityID, int.MinValue) { }
		public ProductQuantity(int productQuantityID, int scratchBookID) : this(productQuantityID, scratchBookID, int.MinValue) { }
		public ProductQuantity(int productQuantityID, int scratchBookID, int quantity) : this(productQuantityID, scratchBookID, quantity, null) { }
		public ProductQuantity(int productQuantityID, int scratchBookID, int quantity, string comments) {
			this.productQuantityID = productQuantityID;
			this.scratchBookID = scratchBookID;
			this.quantity = quantity;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductQuantity>\r\n" +
			"	<ProductQuantityID>" + productQuantityID + "</ProductQuantityID>\r\n" +
			"	<ScratchBookID>" + scratchBookID + "</ScratchBookID>\r\n" +
			"	<Quantity>" + quantity + "</Quantity>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</ProductQuantity>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productQuantityId")) {
					SetXmlValue(ref productQuantityID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantity")) {
					SetXmlValue(ref quantity, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductQuantity[] GetProductQuantitys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductQuantitys();
		}

		public static ProductQuantity GetProductQuantityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductQuantityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductQuantity(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductQuantity(this);
		}
		#endregion

		#region Properties
		public int ProductQuantityID {
			set { productQuantityID = value; }
			get { return productQuantityID; }
		}

		public int ScratchBookID {
			set { scratchBookID = value; }
			get { return scratchBookID; }
		}

		public int Quantity {
			set { quantity = value; }
			get { return quantity; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
