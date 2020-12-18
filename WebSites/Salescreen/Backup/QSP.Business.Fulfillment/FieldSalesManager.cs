using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;


namespace QSP.Business.Fulfillment
{
	public partial class FieldSalesManager
	{
		#region Methods
		public static FieldSalesManager GetFieldSalesManagerByFmId(string fmId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
                c.Add(Expression.Eq(FmIdProperty, fmId));
                c.Add(Expression.Eq(DeletedProperty, false));
                return c.UniqueResult<FieldSalesManager>();
			}
		}

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
            return c;
        }
		#endregion
	}
}
