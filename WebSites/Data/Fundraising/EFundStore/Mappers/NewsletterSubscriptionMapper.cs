using GA.BDC.Data.Fundraising.EFundStore.Tables;
using System;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class NewsletterSubscriptionMapper
    {

        public static NewsletterSubscription Hydrate(newsletter_subscription newslettersubscription)
        {
            var _newslettersubscription = new NewsletterSubscription
            {
                Id = newslettersubscription.subscription_id,
                PartnerId = newslettersubscription.partner_id,
                CultureCode = newslettersubscription.culture_code,
                Referrer = newslettersubscription.referrer,
                Email = newslettersubscription.email,
                Fullname = newslettersubscription.fullname,
                Unsubscribed = newslettersubscription.unsubscribed,
                SubscribeDate = newslettersubscription.subscribe_date,
                UnsubscribeDate = newslettersubscription.unsubscribe_date,

            };
            return _newslettersubscription;
        }

        public static newsletter_subscription DeHydrate(NewsletterSubscription newslettersubscription)
        {
            var _newslettersubscription = new newsletter_subscription
            {
                subscription_id = newslettersubscription.Id,
                partner_id = newslettersubscription.PartnerId,
                culture_code = newslettersubscription.CultureCode,
                referrer = newslettersubscription.Referrer,
                email = newslettersubscription.Email,
                fullname = newslettersubscription.Fullname,
                unsubscribed = newslettersubscription.Unsubscribed,
                subscribe_date = newslettersubscription.SubscribeDate,
                unsubscribe_date = newslettersubscription.UnsubscribeDate,

            };
            return _newslettersubscription;

        }

    }
}
