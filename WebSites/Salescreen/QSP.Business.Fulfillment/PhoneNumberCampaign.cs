using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class PhoneNumberCampaign
    {
        #region Methods

        public static List<PhoneNumberCampaign> GetPhoneNumberCampaignList(int campaignId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberCampaign));
                c.Add(Expression.Eq(CampaignIdProperty, campaignId));

                return (List<PhoneNumberCampaign>)c.List<PhoneNumberCampaign>();
            }
        }

        public static List<PhoneNumberCampaign> GetPhoneNumberCampaignList(int campaignId, int phoneNumberTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberCampaign));
                c.Add(Expression.Eq(CampaignIdProperty, campaignId));
                c.Add(Expression.Eq(PhoneNumberTypeIdProperty, phoneNumberTypeId));

                return (List<PhoneNumberCampaign>)c.List<PhoneNumberCampaign>();
            }
        }

        #endregion
    }
}
