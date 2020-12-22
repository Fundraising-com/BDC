using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
    public partial class ProgramAgreementCampaign
    {
        #region Methods

        public static List<ProgramAgreementCampaign> GetProgramAgreementCampaignList(int campaignId, int programId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ProgramAgreementCampaign));
                c.Add(Expression.Eq(CampaignIdProperty, campaignId));
                c.Add(Expression.Eq(ProgramIdProperty, programId));

                return (List<ProgramAgreementCampaign>)c.List<ProgramAgreementCampaign>();
            }
        }

        #endregion
    }
}
