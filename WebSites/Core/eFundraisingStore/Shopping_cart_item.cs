using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ShoppingCartItem: eFundraisingStoreDataObject {

		private int shoppingCartId;
		private int scratchBookId;
		private short carrierId;
		private short shippingOptionId;
		private short quantity;
		private string clientUploadedImg;
		private string groupName;


		public ShoppingCartItem() : this(int.MinValue) { }
		public ShoppingCartItem(int shoppingCartId) : this(shoppingCartId, int.MinValue) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId) : this(shoppingCartId, scratchBookId, short.MinValue) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId, short carrierId) : this(shoppingCartId, scratchBookId, carrierId, short.MinValue) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId, short carrierId, short shippingOptionId) : this(shoppingCartId, scratchBookId, carrierId, shippingOptionId, short.MinValue) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId, short carrierId, short shippingOptionId, short quantity) : this(shoppingCartId, scratchBookId, carrierId, shippingOptionId, quantity, null) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId, short carrierId, short shippingOptionId, short quantity, string clientUploadedImg) : this(shoppingCartId, scratchBookId, carrierId, shippingOptionId, quantity, clientUploadedImg, null) { }
		public ShoppingCartItem(int shoppingCartId, int scratchBookId, short carrierId, short shippingOptionId, short quantity, string clientUploadedImg, string groupName) {
			this.shoppingCartId = shoppingCartId;
			this.scratchBookId = scratchBookId;
			this.carrierId = carrierId;
			this.shippingOptionId = shippingOptionId;
			this.quantity = quantity;
			this.clientUploadedImg = clientUploadedImg;
			this.groupName = groupName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ShoppingCartItem>\r\n" +
			"	<ShoppingCartId>" + shoppingCartId + "</ShoppingCartId>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
			"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
			"	<Quantity>" + quantity + "</Quantity>\r\n" +
			"	<ClientUploadedImg>" + System.Web.HttpUtility.HtmlEncode(clientUploadedImg) + "</ClientUploadedImg>\r\n" +
			"	<GroupName>" + System.Web.HttpUtility.HtmlEncode(groupName) + "</GroupName>\r\n" +
			"</ShoppingCartItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "shoppingCartId") {
					SetXmlValue(ref shoppingCartId, node.InnerText);
				}
				if(node.Name.ToLower() == "scratchBookId") {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(node.Name.ToLower() == "carrierId") {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(node.Name.ToLower() == "shippingOptionId") {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(node.Name.ToLower() == "quantity") {
					SetXmlValue(ref quantity, node.InnerText);
				}
				if(node.Name.ToLower() == "clientUploadedImg") {
					SetXmlValue(ref clientUploadedImg, node.InnerText);
				}
				if(node.Name.ToLower() == "groupName") {
					SetXmlValue(ref groupName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ShoppingCartItem[] GetShoppingCartItems() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCartItems();
		}

		public static ShoppingCartItem GetShoppingCartItemByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCartItemByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertShoppingCartItem(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateShoppingCartItem(this);
		}
		#endregion

		#region Properties
		public int ShoppingCartId {
			set { shoppingCartId = value; }
			get { return shoppingCartId; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short CarrierId {
			set { carrierId = value; }
			get { return carrierId; }
		}

		public short ShippingOptionId {
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public short Quantity {
			set { quantity = value; }
			get { return quantity; }
		}

		public string ClientUploadedImg {
			set { clientUploadedImg = value; }
			get { return clientUploadedImg; }
		}

		public string GroupName {
			set { groupName = value; }
			get { return groupName; }
		}

		#endregion
	}
}
