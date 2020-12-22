using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class Warehouse
    {

        public static List<Warehouse> GetPickupWarehouseList()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Warehouse));
                c.Add(Expression.Eq(PickUpProperty, true));
                c.Add(Expression.Eq(IsVendorWarehouseProperty, false));
                c.Add(Expression.Eq(WarehouseStatusIdProperty, 301));
                c.Add(Expression.Eq(IsPickUpAvailableForEFRProperty, true));
                return (List<Warehouse>)c.List<Warehouse>();
            }
        }

    }
}
