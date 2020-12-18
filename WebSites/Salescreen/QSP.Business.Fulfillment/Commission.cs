using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class Commission
    {
        #region Methods

        public static List<Commission> GetCommissionListFromOrderDetail(int orderDetailId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Commission));
                c.Add(Expression.Eq(OrderDetailIdProperty, orderDetailId));
                return (List<Commission>)c.List<Commission>();
            }
        }

        #endregion
    }
}
