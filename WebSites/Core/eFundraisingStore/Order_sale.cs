using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class OrderSale: eFundraisingStoreDataObject {

		private int orderId;
		private int saleId;
		private DateTime dateCreated;


		public OrderSale() : this(int.MinValue) { }
		public OrderSale(int orderId) : this(orderId, int.MinValue) { }
		public OrderSale(int orderId, int saleId) : this(orderId, saleId, DateTime.MinValue) { }
		public OrderSale(int orderId, int saleId, DateTime dateCreated) {
			this.orderId = orderId;
			this.saleId = saleId;
			this.dateCreated = dateCreated;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrderSale>\r\n" +
			"	<OrderId>" + orderId + "</OrderId>\r\n" +
			"	<SaleId>" + saleId + "</SaleId>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"</OrderSale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "orderId") {
					SetXmlValue(ref orderId, node.InnerText);
				}
				if(node.Name.ToLower() == "saleId") {
					SetXmlValue(ref saleId, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrderSale[] GetOrderSales() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrderSales();
		}

		public static OrderSale GetOrderSaleByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrderSaleByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOrderSale(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOrderSale(this);
		}
		#endregion

		#region Properties
		public int OrderId {
			set { orderId = value; }
			get { return orderId; }
		}

		public int SaleId {
			set { saleId = value; }
			get { return saleId; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		#endregion
	}
}
