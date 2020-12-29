using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using GA.BDC.Data.EzFund.EZMain.Tables;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class NewsletterSubscriptionRepository : INewsletterSubscriptionRepository
    {
        private readonly DataProvider _dataProvider;

        public NewsletterSubscriptionRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(NewsletterSubscription model)
        {
            throw new NotImplementedException();
        }

        public IList<NewsletterSubscription> GetAll()
        {
            throw new NotImplementedException();
        }

        public NewsletterSubscription GetById(int id)
        {
            const string sql = @"SELECT TOP 1 * FROM newsletter_subscription (NOLOCK) WHERE SUBSCRIPTION_ID = @id";
            var row = _dataProvider.Database.Connection.QueryFirstOrDefault<newsletter_subscription>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            if (row != null)
                return NewsletterSubscriptionMapper.Hydrate(row);
            else
                return null;
        }

        public NewsletterSubscription GetSubscriberByMail(string mail)
        {
            const string sql = @"SELECT SUBSCRIPTION_ID FROM newsletter_subscription AS PGM (NOLOCK) WHERE EMAIL = @mail";
            var id = _dataProvider.Database.Connection.QueryFirstOrDefault<int>(sql, new { mail }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
      
        }

        public int Save(NewsletterSubscription model)
        {
            model.SubscribeDate = DateTime.Now;
            var row = NewsletterSubscriptionMapper.Dehydrate(model);
            try
            {
                var subscriptionId = _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                return (int)subscriptionId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(NewsletterSubscription model)
        {
            throw new NotImplementedException();
        }
    }
}
