using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Expression;
namespace QSP.Business.Fulfillment
{
    partial class Product
    {

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Product));
            return c;
        }
    }
}
