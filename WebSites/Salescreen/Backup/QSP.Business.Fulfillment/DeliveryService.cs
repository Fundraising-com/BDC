using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class DeliveryService
    {
        #region Methods

        public static List<DeliveryService> GetDeliveryServiceListFromZipCode(string zipCode, int formId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(DeliveryService));
                c.Add(Expression.Eq(ZipCodeProperty, zipCode));
                c.Add(Expression.Eq(FormIdProperty, formId));
                return (List<DeliveryService>)c.List<DeliveryService>();
            }
        }

        #endregion
    }
}
