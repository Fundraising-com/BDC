using System;
using System.Collections.Generic;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
    public class NewsletterSubscriptionRepository : INewsletterSubscriptionRepository
    {

        private readonly DataProvider _dataProvider;
        public NewsletterSubscriptionRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


       public NewsletterSubscription GetById(int id)
       {
          throw new NotImplementedException();
       }

       public IList<NewsletterSubscription> GetAll()
       {
          throw new NotImplementedException();
       }

       public int Save(NewsletterSubscription newslettersubscription)
        {

            newslettersubscription.SubscribeDate = DateTime.Now;
            var newslettersubscriptionToBePersisted = NewsletterSubscriptionMapper.DeHydrate(newslettersubscription);
            newslettersubscriptionToBePersisted.unsubscribe_date = null;

	        _dataProvider.newsletter_subscription.Add(newslettersubscriptionToBePersisted);
	        _dataProvider.SaveChanges();
	        return newslettersubscriptionToBePersisted.subscription_id;
        }

       public void Update(NewsletterSubscription model)
       {
          throw new NotImplementedException();
       }

       public void Delete(NewsletterSubscription model)
       {
          throw new NotImplementedException();
       }

        public NewsletterSubscription GetSubscriberByMail(string mail)
        {
            throw new NotImplementedException();
        }
    }
}
