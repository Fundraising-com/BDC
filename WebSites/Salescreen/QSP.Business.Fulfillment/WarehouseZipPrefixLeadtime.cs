using System;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class WarehouseZipPrefixLeadtime
    {
        #region Methods

        public static List<WarehouseZipPrefixLeadtime> GetWarehouseZipPrefixLeadtimeFromZipPrefixId(int zipPrefixId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(WarehouseZipPrefixLeadtime));
                c.Add(Expression.Eq(ZipPrefixIDProperty, zipPrefixId));
                c.Add(Expression.Le(StartDateProperty, DateTime.Now));
                c.Add(Expression.Ge(EndDateProperty, DateTime.Now));
                c.Add(Expression.Eq(DisabledProperty, false));

                return (List<WarehouseZipPrefixLeadtime>)c.List<WarehouseZipPrefixLeadtime>();
            }
        }

        #endregion
    }
}
