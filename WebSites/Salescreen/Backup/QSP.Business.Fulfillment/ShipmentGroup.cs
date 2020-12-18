using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class ShipmentGroup
    {

        #region Methods

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(ShipmentGroup));
            return c;
        }

        #endregion

    }
}
