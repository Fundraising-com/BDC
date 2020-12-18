using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class Order
    {
        #region Methods

        public static List<Order> GetOrderListFromCustomer(int customerId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Order));
                c.Add(Expression.Eq(CustomerIdProperty, customerId));
                return (List<Order>)c.List<Order>();
            }
        }
        public static List<Order> GetOrderListFromCampaignAndForm(int campaignId, int formId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Order));

                c.Add(Expression.Eq(CampaignIdProperty, campaignId));
                c.Add(Expression.Eq(FormIdProperty, formId));
                c.Add(Expression.Gt(OrderStatusIdProperty, 100));
                
                return (List<Order>)c.List<Order>();
            }
        }

        public static void SetOrderToWaitForApproval(int orderId)
        {
            int a = 0;

            Order order = Order.GetOrder(orderId);
            order.OrderStatusId = 5;
            Order.UpdateOrder(order);
        }

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Order));
            return c;
        }

        #endregion

        #region Extended properties

        //private List<OrderDetail> orderDetails;

        //public List<OrderDetail> OrderDetails
        //{
        //    get { return OrderDetails; }
        //    set { orderDetails = value; }
        //}

        #endregion
    }
}
