using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
     
    
    public class NewsletterRepository : INewsletterRepository
    {

        private readonly DataProvider _dataProvider;
        public NewsletterRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }       
        
        
        public IList<Newsletter> GetAll()
        {
            var result = new List<Newsletter>();
            var newslettersIds = _dataProvider.newsletters.Where(p => p.id > 0 && p.enabled).Select(p => p.id).ToList();
            foreach (var newsletterId in newslettersIds)
            {
                var news = GetById(newsletterId);
                result.Add(news);
            }
            return result;
        }

       public int Save(Newsletter model)
       {
          throw new NotImplementedException();
       }

       public void Update(Newsletter model)
       {
          throw new NotImplementedException();
       }

       public void Delete(Newsletter model)
       {
          throw new NotImplementedException();
       }


       public IList<Newsletter> GetByPartner(int partnerId)
        {
            var result = new List<Newsletter>();
            var newslettersIds = _dataProvider.newsletters.Where(p => p.partner == partnerId && p.enabled).Select(p => p.id).ToList();
             foreach (var newsletterId in newslettersIds)
            {
                var news = GetById(newsletterId);
                result.Add(news);
            }
            return result;
        }
  

        public Newsletter GetById(int id)
        {
            var newsletter = _dataProvider.newsletters.Find(id);
            var result = NewsletterMapper.Hydrate(newsletter);
            return result;


        }

        public Newsletter GetByUrl(string url)
        {
            var newsletterId = _dataProvider.newsletters.FirstOrDefault(p => p.url == url && p.enabled).id;
            var newsletter = GetById(newsletterId);
            return newsletter;
   
           
        }
    }
}
