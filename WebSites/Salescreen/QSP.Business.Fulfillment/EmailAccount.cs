using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{

    public partial class EmailAccount
    {
        #region Methods

        public static List<EmailAccount> GetEmailAccountListByAccount(int accountId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(EmailAccount));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                return (List<EmailAccount>)c.List<EmailAccount>();
            }
        }

        #endregion
    }

}
