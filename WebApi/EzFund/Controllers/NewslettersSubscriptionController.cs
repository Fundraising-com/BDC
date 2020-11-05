using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class NewslettersSubscriptionController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(NewsletterSubscription model)
        {
            var subscriptionId = 0;
            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var newsletterRepository = eZFundProdUnitOfWork.CreateRepository<INewsletterSubscriptionRepository>();
                subscriptionId = newsletterRepository.Save(model);
                eZFundProdUnitOfWork.Commit();
            }
            return Ok(subscriptionId);
        }

        [HttpGet]
        public IHttpActionResult GetSubscriberByMail(string email)
        {
            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var newsletterRepository = eZFundProdUnitOfWork.CreateRepository<INewsletterSubscriptionRepository>();
                return Ok(newsletterRepository.GetSubscriberByMail(email));
            }
        }
    }
}
