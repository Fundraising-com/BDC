using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    partial class User
    {

        #region Methods
        public static User GetUserId(string userId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(User));
                c.Add(Expression.Eq(UserIdProperty, userId));
                c.Add(Expression.Eq(DeletedProperty, false));
                return c.UniqueResult<User>();
            }
        }

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(User));
            return c;
        }
        #endregion


    }
}
