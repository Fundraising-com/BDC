using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class OrdersSale: EFundraisingCRMDataObject {

		private int orderId;
		private int salesId;
		private DateTime dateCreated;


		public OrdersSale() : this(int.MinValue) { }
		public OrdersSale(int orderId) : this(orderId, int.MinValue) { }
		public OrdersSale(int orderId, int salesId) : this(orderId, salesId, DateTime.MinValue) { }
		public OrdersSale(int orderId, int salesId, DateTime dateCreated) {
			this.orderId = orderId;
			this.salesId = salesId;
			this.dateCreated = dateCreated;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrdersSale>\r\n" +
			"	<OrderId>" + orderId + "</OrderId>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"</OrdersSale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("orderId")) {
					SetXmlValue(ref orderId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateCreated")) {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrdersSale[] GetOrdersSales() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrdersSales();
		}

		public static OrdersSale GetOrdersSaleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrdersSaleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrdersSale(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrdersSale(this);
		}
		#endregion

		#region Properties
		public int OrderId {
			set { orderId = value; }
			get { return orderId; }
		}

		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		#endregion
	}
}
