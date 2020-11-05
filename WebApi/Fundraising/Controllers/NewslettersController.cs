using System;
using System.Collections.Generic;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class NewslettersController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IList<Newsletter> GetAll()
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var newsletterRepository = efundStoreUnitOfWork.CreateRepository<INewsletterRepository>();
            return newsletterRepository.GetAll();
         }
      }

      [HttpGet]
      public IList<Newsletter> GetByPartner(int partnerId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var newsletterRepository = efundStoreUnitOfWork.CreateRepository<INewsletterRepository>();
            return newsletterRepository.GetByPartner(partnerId);
         }
      }

      [HttpGet]
      public Newsletter Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var newsletterRepository = efundStoreUnitOfWork.CreateRepository<INewsletterRepository>();
            return newsletterRepository.GetById(id);
         }

      }
      [HttpGet]
      public IHttpActionResult GetByUrl(string url)
      {
         try
         {
            using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
               var newsletterRepository = efundStoreUnitOfWork.CreateRepository<INewsletterRepository>();
               var newletter = newsletterRepository.GetByUrl(url);
               return Ok(newletter);
            }
         }
         catch (Exception)
         {
            return BadRequest();
         }
      }
   }
}
