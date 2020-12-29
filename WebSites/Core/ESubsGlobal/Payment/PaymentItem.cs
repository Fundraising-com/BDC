using System;
using System.Xml;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Payment
{
	public class PaymentItem: DataObject {

		private int paymentItemId;
		private int paymentId;
		private int qspOrderId;
		private DateTime createDate;
		private int qspOrderDetailId;
		private double orderDetailAmount;
		private double profitPercentage;
		private double profitAmount;
        private int profitId;
        private int profitRangeId;

		public PaymentItem() : this(int.MinValue) { }
		public PaymentItem(int paymentItemId) : this(paymentItemId, int.MinValue) { }
		public PaymentItem(int paymentItemId, int paymentId) : this(paymentItemId, paymentId, int.MinValue) { }
		public PaymentItem(int paymentItemId, int paymentId, int qspOrderId) : this(paymentItemId, paymentId, qspOrderId, DateTime.MinValue) { }
		public PaymentItem(int paymentItemId, int paymentId, int qspOrderId, DateTime createDate) {
			this.paymentItemId = paymentItemId;
			this.paymentId = paymentId;
			this.qspOrderId = qspOrderId;
			this.createDate = createDate;
		}


		public PaymentItem(int paymentItemId, int paymentId, double profitAmount, double profitPercentage, double orderDetailAmount,
			int qspOrderDetailId, DateTime createDate, int profitId, int profitRangeID)
		{
			this.paymentItemId = paymentItemId;
			this.paymentId = paymentId;
			this.qspOrderDetailId = qspOrderDetailId;
			this.profitAmount = profitAmount;
			this.ProfitPercentage = profitPercentage;
			this.OrderDetailAmount = orderDetailAmount;
			this.createDate = createDate;
            this.profitId = profitId;
            this.ProfitRangeId = profitRangeID;
		}

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentItem>\r\n" +
				"	<PaymentItemId>" + paymentItemId + "</PaymentItemId>\r\n" +
				"	<PaymentId>" + paymentId + "</PaymentId>\r\n" +
				"	<QspOrderId>" + qspOrderId + "</QspOrderId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PaymentItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "paymentItemId") {
					SetXmlValue(ref paymentItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentId") {
					SetXmlValue(ref paymentId, node.InnerText);
				}
				if(node.Name.ToLower() == "qspOrderId") {
					SetXmlValue(ref qspOrderId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PaymentItem[] GetPaymentItems() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentItems();
		}

		public static PaymentItem GetPaymentItemByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentItemByID(id);
		}

        public static PaymentItem GetLastPaymentItemByEventID(int eventId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetLastPaymentItemByEventID(eventId);
        }

		public static PaymentItem GetPaymentItemByQSPOrderDetailID(int orderDetailId)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentItemByQSPOrderDetailID(orderDetailId);
		}
		
		public static PaymentItem[] GetPaymentItemsByPaymentID(int paymentId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentItemsByPaymentId(paymentId);
		}

        public static List<PaymentItem> GetProcessedPaymentItemsByEventId(int eventID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetProcessedPaymentItemsByEventId(eventID);
        }
		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentItem(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentItem(this);
		}
		#endregion

		#region Properties
		public double ProfitAmount
		{
			set { profitAmount = value;}
			get { return profitAmount;}
		}
		public double ProfitPercentage
		{
			set { profitPercentage = value;}
			get { return profitPercentage;}
		}
		public double OrderDetailAmount
		{
			set { orderDetailAmount = value;}
			get { return orderDetailAmount;}
		}

		public int QspOrderDetailId
		{
			set { qspOrderDetailId = value;}
			get { return qspOrderDetailId; }
		}

		public int PaymentItemId {
			set { paymentItemId = value; }
			get { return paymentItemId; }
		}

		public int PaymentId {
			set { paymentId = value; }
			get { return paymentId; }
		}

		public int QspOrderId {
			set { qspOrderId = value; }
			get { return qspOrderId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

        public int ProfitId
        {
            set { profitId = value; }
            get { return profitId; }
        }

        public int ProfitRangeId
        {
            set { profitRangeId = value; }
            get { return profitRangeId; }
        }


		#endregion
	}
}
