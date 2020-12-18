using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class PhoneNumberAccount
    {
        #region Methods

        public static List<PhoneNumberAccount> GetPhoneNumberAccountList(int accountId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberAccount));
                c.Add(Expression.Eq(AccountIdProperty, accountId));

                return (List<PhoneNumberAccount>)c.List<PhoneNumberAccount>();
            }
        }

        public static List<PhoneNumberAccount> GetPhoneNumberAccountList(int accountId, int phoneNumberTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberAccount));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                c.Add(Expression.Eq(PhoneNumberTypeIdProperty, phoneNumberTypeId));

                return (List<PhoneNumberAccount>)c.List<PhoneNumberAccount>();
            }
        }

        #endregion
    }
}
