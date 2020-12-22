using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
	public partial class Account
	{
		#region Methods

		public static Account GetAccountByFulfillmentAccountId(int fulfillmentAccountId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(Account));
                c.Add(Expression.Eq(FulfAccountIdProperty, fulfillmentAccountId));
                return c.UniqueResult<Account>();
			}
		}


        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Account));
            return c;
        }


		#endregion
	}
}
