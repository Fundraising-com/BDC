using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class Campaign
    {
        #region Methods

        public static List<Campaign> GetCampaignList(int fiscalYear, int accountId, int programTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Campaign));
                c.Add(Expression.Eq(FiscalYearProperty, fiscalYear));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                c.Add(Expression.Eq(ProgramTypeIdProperty, programTypeId));

                return (List<Campaign>)c.List<Campaign>();
            }
        }
        public static List<Campaign> GetCampaignList(int accountId, int programTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Campaign));
                c.Add(Expression.Eq(AccountIdProperty, accountId));
                c.Add(Expression.Eq(ProgramTypeIdProperty, programTypeId));

                return (List<Campaign>)c.List<Campaign>();
            }
        }
        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Campaign));
            return c;
        }

        #endregion
    }
}
