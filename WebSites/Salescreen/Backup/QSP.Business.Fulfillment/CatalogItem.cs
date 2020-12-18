using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class CatalogItem
    {

        #region Methods

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(CatalogItem));
            return c;
        }

        public static List<CatalogItem> GetCatalogItemListByCatalogId(int catalogId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CatalogItem));

                c.Add(Expression.Eq(CatalogIdProperty, catalogId));

                return (List<CatalogItem>)c.List<CatalogItem>();
            }
        }

        #endregion

    }
}