using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface INewsletterSubscriptionRepository : IRepository<NewsletterSubscription>
   {
        NewsletterSubscription GetSubscriberByMail(string mail);
   }
}
