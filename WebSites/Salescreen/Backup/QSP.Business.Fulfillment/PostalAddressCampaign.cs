using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class PostalAddressCampaign
    {
        #region Methods

        public static List<PostalAddressCampaign> GetAddressesByTypeAndCampaign(int postalAddressType, int campaignId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PostalAddressCampaign));
                c.Add(Expression.Eq(PostalAddressTypeIdProperty, postalAddressType));
                c.Add(Expression.Eq(CampaignIdProperty, campaignId));
                return (List<PostalAddressCampaign>)c.List<PostalAddressCampaign>();
            }
        }

        #endregion
    }
}
