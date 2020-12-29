using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class OrderCoupon: eFundraisingStoreDataObject {

		private int orderId;
		private int couponId;


		public OrderCoupon() : this(int.MinValue) { }
		public OrderCoupon(int orderId) : this(orderId, int.MinValue) { }
		public OrderCoupon(int orderId, int couponId) {
			this.orderId = orderId;
			this.couponId = couponId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OrderCoupon>\r\n" +
			"	<OrderId>" + orderId + "</OrderId>\r\n" +
			"	<CouponId>" + couponId + "</CouponId>\r\n" +
			"</OrderCoupon>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "orderId") {
					SetXmlValue(ref orderId, node.InnerText);
				}
				if(node.Name.ToLower() == "couponId") {
					SetXmlValue(ref couponId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OrderCoupon[] GetOrderCoupons() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrderCoupons();
		}

		public static OrderCoupon GetOrderCouponByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOrderCouponByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOrderCoupon(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOrderCoupon(this);
		}
		#endregion

		#region Properties
		public int OrderId {
			set { orderId = value; }
			get { return orderId; }
		}

		public int CouponId {
			set { couponId = value; }
			get { return couponId; }
		}

		#endregion
	}
}
