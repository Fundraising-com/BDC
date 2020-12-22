



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;


namespace QSP.Business.Fulfillment
{
    public partial class CatalogItemDetail
    {

        #region Methods

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(CatalogItemDetail));
            return c;
        }

        public static CatalogItemDetail GetCatalogItemDetailByCatalogItemIdAndProfitRate(int catalogItemId, double profitRate)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CatalogItemDetail));

                c.Add(Expression.Eq(CatalogItemIdProperty, catalogItemId));
                c.Add(Expression.Eq(ProfitRateProperty, profitRate));

                return (CatalogItemDetail)c.UniqueResult<CatalogItemDetail>();
            }
        }

        #endregion

    }
}
