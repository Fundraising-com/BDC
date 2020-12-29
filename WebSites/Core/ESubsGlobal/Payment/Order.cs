using System;

namespace GA.BDC.Core.ESubsGlobal.Payment
{
    /// <summary>
    /// Summary description for Orders.
    /// </summary>
    public class Order
    {
        private int orderID = int.MinValue;
        private int quantity = int.MinValue;
        private float price;
        private int eventParticipationID = int.MinValue;
        private DateTime orderDate;
        private int customerID = int.MinValue;
        private string itemName;
        private string customerFirstName;
        private string customerLastName;
        private int orderItemID = int.MinValue;
        private int orderDetailID = int.MinValue;
        private int orderStatusID = int.MinValue;
        private int edsID = int.MinValue;
        private double orderDetailAmount;
        //private int productTypeId;  
        private decimal fulfillmentCharge;


        public Order()
        {

        }

        public static Order[] GetOrders(int groupID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetOrdersByGroup(groupID);
        }


        //		public static Order[] GetOrdersByEventId(int EventId) 
        //		{
        //			DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
        //			return dbo.GetOrdersByEventId(EventId);
        //		}

        public static Order[] GetOrderDetailByEventId(int EventId, DateTime startDate, DateTime endDate)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetOrderDetailByEventId(EventId, startDate, endDate);
        }

        public static Order[] GetOrderDetailByEventId(int EventId, DateTime startDate, DateTime endDate, bool includePayments)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetOrderDetailByEventId(EventId, startDate, endDate, includePayments);
        }


        public static Order[] GetOrdersDetailCanceledByEventId(int eventId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetOrdersDetailCanceledByEventId(eventId);
        }


       
        #region Properties

        public double OrderDetailAmount
        {
            set { orderDetailAmount = value; }
            get { return orderDetailAmount; }
        }

        public int OrderID
        {
            set { orderID = value; }
            get { return orderID; }
        }

        public int Quantity
        {
            set { quantity = value; }
            get { return quantity; }
        }

        public float Price
        {
            set { price = value; }
            get { return price; }
        }

        public int EventParticipationID
        {
            set { eventParticipationID = value; }
            get { return eventParticipationID; }
        }

        public DateTime OrderDate
        {
            set { orderDate = value; }
            get { return orderDate; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string CustomerFirstName
        {
            get { return customerFirstName; }
            set { customerFirstName = value; }
        }

        public string CustomerLastName
        {
            get { return customerLastName; }
            set { customerLastName = value; }
        }

        public int OrderItemID
        {
            get { return orderItemID; }
            set { orderItemID = value; }
        }

        public int OrderDetailID
        {
            get { return orderDetailID; }
            set { orderDetailID = value; }
        }

        public int OrderStatusID
        {
            get { return orderStatusID; }
            set { orderStatusID = value; }
        }

        public int EdsID
        {
            get { return edsID; }
            set { edsID = value; }
        }

        //public int ProductTypeId
        //{
        //    set { productTypeId = value; }
        //    get { return productTypeId; }
        //}

        public decimal FulfillmentCharge
        {
            set { fulfillmentCharge = value; }
            get { return fulfillmentCharge; }
        }



        #endregion
    }
}
