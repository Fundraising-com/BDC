using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public class NewsletterSubscriptionMapper
    {
        public static NewsletterSubscription Hydrate(newsletter_subscription newsletterSubscription)
        {
            return new NewsletterSubscription
            {
                Id = newsletterSubscription.subscription_id,
                PartnerId = newsletterSubscription.partner_id,
                Referrer = newsletterSubscription.referrer,
                Email = newsletterSubscription.email,
                Fullname = newsletterSubscription.fullname,
                Unsubscribed = newsletterSubscription.unsubscribed,
                SubscribeDate = newsletterSubscription.subscribed_date,
                UnsubscribeDate = newsletterSubscription.unsubscribe_date,
            };
        }
        public static newsletter_subscription Dehydrate(NewsletterSubscription newsletterSubscription)
        {
            return new newsletter_subscription
            {
                partner_id = newsletterSubscription.PartnerId,
                referrer = newsletterSubscription.Referrer,
                email = newsletterSubscription.Email,
                fullname = newsletterSubscription.Fullname,
                unsubscribed = newsletterSubscription.Unsubscribed,
                subscribed_date = newsletterSubscription.SubscribeDate,
                unsubscribe_date = newsletterSubscription.UnsubscribeDate,
            };
        }
    }
}
