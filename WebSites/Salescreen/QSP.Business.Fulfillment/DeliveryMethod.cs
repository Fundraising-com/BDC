﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
namespace QSP.Business.Fulfillment
{
    partial class DeliveryMethod
    {


        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(DeliveryMethod));
            return c;
        }

    }
}
