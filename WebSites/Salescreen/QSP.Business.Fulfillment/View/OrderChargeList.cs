using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment.View
{
    public partial class OrderChargeList
	{
		#region Methods

        /// <summary>
        /// Gets the order charge list that belongs to the specified order
        /// </summary>
        /// <param name="orderId">The id of the order to get the order charges from</param>
        /// <returns>A list of order charges</returns>
        public static List<OrderChargeList> GetOrderChargeListListByOrder(int orderId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderChargeList));
                c.Add(Expression.Eq(OrderIdProperty, orderId));

                return (List<OrderChargeList>)c.List<OrderChargeList>();
            }
        }

		#endregion
	}
}
