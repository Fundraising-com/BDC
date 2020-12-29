using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ShoppingCart: eFundraisingStoreDataObject {

		private int shoppingCartId;
		private int visitorLogId;
		private int onlineUserId;
		private string shoppingCartCode;
		private DateTime dateCreated;


		public ShoppingCart() : this(int.MinValue) { }
		public ShoppingCart(int shoppingCartId) : this(shoppingCartId, int.MinValue) { }
		public ShoppingCart(int shoppingCartId, int visitorLogId) : this(shoppingCartId, visitorLogId, int.MinValue) { }
		public ShoppingCart(int shoppingCartId, int visitorLogId, int onlineUserId) : this(shoppingCartId, visitorLogId, onlineUserId, null) { }
		public ShoppingCart(int shoppingCartId, int visitorLogId, int onlineUserId, string shoppingCartCode) : this(shoppingCartId, visitorLogId, onlineUserId, shoppingCartCode, DateTime.MinValue) { }
		public ShoppingCart(int shoppingCartId, int visitorLogId, int onlineUserId, string shoppingCartCode, DateTime dateCreated) {
			this.shoppingCartId = shoppingCartId;
			this.visitorLogId = visitorLogId;
			this.onlineUserId = onlineUserId;
			this.shoppingCartCode = shoppingCartCode;
			this.dateCreated = dateCreated;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ShoppingCart>\r\n" +
			"	<ShoppingCartId>" + shoppingCartId + "</ShoppingCartId>\r\n" +
			"	<VisitorLogId>" + visitorLogId + "</VisitorLogId>\r\n" +
			"	<OnlineUserId>" + onlineUserId + "</OnlineUserId>\r\n" +
			"	<ShoppingCartCode>" + System.Web.HttpUtility.HtmlEncode(shoppingCartCode) + "</ShoppingCartCode>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"</ShoppingCart>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "shoppingCartId") {
					SetXmlValue(ref shoppingCartId, node.InnerText);
				}
				if(node.Name.ToLower() == "visitorLogId") {
					SetXmlValue(ref visitorLogId, node.InnerText);
				}
				if(node.Name.ToLower() == "onlineUserId") {
					SetXmlValue(ref onlineUserId, node.InnerText);
				}
				if(node.Name.ToLower() == "shoppingCartCode") {
					SetXmlValue(ref shoppingCartCode, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ShoppingCart[] GetShoppingCarts() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCarts();
		}

		public static ShoppingCart GetShoppingCartByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetShoppingCartByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertShoppingCart(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateShoppingCart(this);
		}
		#endregion

		#region Properties
		public int ShoppingCartId {
			set { shoppingCartId = value; }
			get { return shoppingCartId; }
		}

		public int VisitorLogId {
			set { visitorLogId = value; }
			get { return visitorLogId; }
		}

		public int OnlineUserId {
			set { onlineUserId = value; }
			get { return onlineUserId; }
		}

		public string ShoppingCartCode {
			set { shoppingCartCode = value; }
			get { return shoppingCartCode; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		#endregion
	}
}
