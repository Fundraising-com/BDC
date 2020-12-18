using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
	public partial class OrderDetailTax
    {
        #region Methods

        public static List<OrderDetailTax> GetOrderDetailTaxListFromOrderDetail(int orderDetailId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderDetailTax));
                c.Add(Expression.Eq(OrderDetailIdProperty, orderDetailId));
                return (List<OrderDetailTax>)c.List<OrderDetailTax>();
            }
        }

        #endregion
    }
}
