using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class OrderCharge
    {
        #region Methods

        public static List<OrderCharge> GetOrderChargeListFromOrder(int orderId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderCharge));
                c.Add(Expression.Eq(OrderIdProperty, orderId));
                return (List<OrderCharge>)c.List<OrderCharge>();
            }
        }

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(OrderCharge));
            return c;
        }



        #endregion
    }
}
