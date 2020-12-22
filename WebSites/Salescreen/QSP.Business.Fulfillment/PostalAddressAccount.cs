using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class PostalAddressAccount
    {
        #region Methods

        public static List<PostalAddressAccount> GetAddressesByAccount(int accountId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PostalAddressAccount));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                return (List<PostalAddressAccount>)c.List<PostalAddressAccount>();
            }
        }

        public static List<PostalAddressAccount> GetAddressesByTypeAndAccount(int postalAddressType, int accountId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PostalAddressAccount));
                c.Add(Expression.Eq(PostalAddressTypeIdProperty, postalAddressType));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                return (List<PostalAddressAccount>)c.List<PostalAddressAccount>();
            }
        }

        #endregion
    }
}
