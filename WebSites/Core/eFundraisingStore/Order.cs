using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Order: eFundraisingStoreDataObject {

		private int orderId;
		private int shoppingCartId;
		private int onlineUserId;
		private int creditCardId;
		private string cultureCode;
		private int randomNumber;
		private string orderNumber;
		private double orderTotal;
		private double shippingTotal;
		private double taxTotal;
		private short orderSubmitted;
		private DateTime dateCreated;
		private DateTime scheduledDeliveryDate;


		public Order() : this(int.MinValue) { }
		public Order(int orderId) : this(orderId, int.MinValue) { }
		public Order(int orderId, int shoppingCartId) : this(orderId, shoppingCartId, int.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId) : this(orderId, shoppingCartId, onlineUserId, int.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId) : this(orderId, shoppingCartId, onlineUserId, creditCardId, null) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, int.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, null) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, double.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, orderTotal, double.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal, double shippingTotal) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, orderTotal, shippingTotal, double.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal, double shippingTotal, double taxTotal) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, orderTotal, shippingTotal, taxTotal, short.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal, double shippingTotal, double taxTotal, short orderSubmitted) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, orderTotal, shippingTotal, taxTotal, orderSubmitted, DateTime.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal, double shippingTotal, double taxTotal, short orderSubmitted, DateTime dateCreated) : this(orderId, shoppingCartId, onlineUserId, creditCardId, cultureCode, randomNumber, orderNumber, orderTotal, shippingTotal, taxTotal, orderSubmitted, dateCreated, DateTime.MinValue) { }
		public Order(int orderId, int shoppingCartId, int onlineUserId, int creditCardId, string cultureCode, int randomNumber, string orderNumber, double orderTotal, double shippingTotal, double taxTotal, short orderSubmitted, DateTime dateCreated, DateTime scheduledDeliveryDate) {
			this.orderId = orderId;
			this.shoppingCartId = shoppingCartId;
			this.onlineUserId = onlineUserId;
			this.creditCardId = creditCardId;
			this.cultureCode = cultureCode;
			this.randomNumber = randomNumber;
			this.orderNumber = orderNumber;
			this.orderTotal = orderTotal;
			this.shippingTotal = shippingTotal;
			this.taxTotal = taxTotal;
			this.orderSubmitted = orderSubmitted;
			this.dateCreated = dateCreated;
			this.scheduledDeliveryDate = scheduledDeliveryDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Order>\r\n" +
			"	<OrderId>" + orderId + "</OrderId>\r\n" +
			"	<ShoppingCartId>" + shoppingCartId + "</ShoppingCartId>\r\n" +
			"	<OnlineUserId>" + onlineUserId + "</OnlineUserId>\r\n" +
			"	<CreditCardId>" + creditCardId + "</CreditCardId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<RandomNumber>" + randomNumber + "</RandomNumber>\r\n" +
			"	<OrderNumber>" + System.Web.HttpUtility.HtmlEncode(orderNumber) + "</OrderNumber>\r\n" +
			"	<OrderTotal>" + orderTotal + "</OrderTotal>\r\n" +
			"	<ShippingTotal>" + shippingTotal + "</ShippingTotal>\r\n" +
			"	<TaxTotal>" + taxTotal + "</TaxTotal>\r\n" +
			"	<OrderSubmitted>" + orderSubmitted + "</OrderSubmitted>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"	<ScheduledDeliveryDate>" + scheduledDeliveryDate + "</ScheduledDeliveryDate>\r\n" +
			"</Order>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "orderId") {
					SetXmlValue(ref orderId, node.InnerText);
				}
				if(node.Name.ToLower() == "shoppingCartId") {
					SetXmlValue(ref shoppingCartId, node.InnerText);
				}
				if(node.Name.ToLower() == "onlineUserId") {
					SetXmlValue(ref onlineUserId, node.InnerText);
				}
				if(node.Name.ToLower() == "creditCardId") {
					SetXmlValue(ref creditCardId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "randomNumber") {
					SetXmlValue(ref randomNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "orderNumber") {
					SetXmlValue(ref orderNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "orderTotal") {
					SetXmlValue(ref orderTotal, node.InnerText);
				}
				if(node.Name.ToLower() == "shippingTotal") {
					SetXmlValue(ref shippingTotal, node.InnerText);
				}
				if(node.Name.ToLower() == "taxTotal") {
					SetXmlValue(ref taxTotal, node.InnerText);
				}
				if(node.Name.ToLower() == "orderSubmitted") {
					SetXmlValue(ref orderSubmitted, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
				if(node.Name.ToLower() == "scheduledDeliveryDate") {
					SetXmlValue(ref scheduledDeliveryDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Order[] GetOrders() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrders();
		}

		public static Order GetOrderByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrderByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOrder(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOrder(this);
		}
		#endregion

		#region Properties
		public int OrderId {
			set { orderId = value; }
			get { return orderId; }
		}

		public int ShoppingCartId {
			set { shoppingCartId = value; }
			get { return shoppingCartId; }
		}

		public int OnlineUserId {
			set { onlineUserId = value; }
			get { return onlineUserId; }
		}

		public int CreditCardId {
			set { creditCardId = value; }
			get { return creditCardId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public int RandomNumber {
			set { randomNumber = value; }
			get { return randomNumber; }
		}

		public string OrderNumber {
			/* read only because sql auto calculated field */
			get { return orderNumber; }
		}

		public double OrderTotal {
			set { orderTotal = value; }
			get { return orderTotal; }
		}

		public double ShippingTotal {
			set { shippingTotal = value; }
			get { return shippingTotal; }
		}

		public double TaxTotal {
			set { taxTotal = value; }
			get { return taxTotal; }
		}

		public short OrderSubmitted {
			set { orderSubmitted = value; }
			get { return orderSubmitted; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		public DateTime ScheduledDeliveryDate {
			set { scheduledDeliveryDate = value; }
			get { return scheduledDeliveryDate; }
		}

		#endregion
	}
}
