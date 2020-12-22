using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
	public partial class OrderDetail
	{
        #region Methods

        public static List<OrderDetail> GetOrderDetailListFromOrder(int orderId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderDetail));
                c.Add(Expression.Eq(OrderIdProperty, orderId));
                return (List<OrderDetail>)c.List<OrderDetail>();
            }
        }

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(OrderDetail));
            return c;

        }

        #endregion
	}
}
