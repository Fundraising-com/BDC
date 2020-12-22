using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class CatalogItemCategoryCatalogItem
    {

        #region Methods

        public static CatalogItemCategoryCatalogItem GetCatalogItemCategoryCatalogItemByCatalogItemCategoryIdAndCatalogItemId(int catalogItemCategoryId, int catalogItemId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CatalogItemCategoryCatalogItem));

                c.Add(Expression.Eq(CatalogItemCategoryIdProperty, catalogItemCategoryId));
                c.Add(Expression.Eq(CatalogItemIdProperty, catalogItemId));

                return (CatalogItemCategoryCatalogItem)c.UniqueResult<CatalogItemCategoryCatalogItem>();
            }
        }

        #endregion

    }
}
