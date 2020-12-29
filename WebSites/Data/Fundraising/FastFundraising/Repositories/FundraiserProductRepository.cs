using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.FastFundraising.Mappers;
using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.FastFundraising.Repositories
{
    public class FundraiserProductRepository : IFundraiserProductRepository
    {
        private readonly DataProvider _dataProvider;
        public FundraiserProductRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IList<FundraiserProduct> GetAllByCategory(int categoryId)
        {
            var productsId =_dataProvider.fundraising_products.Where(p => p.fundraiser_category_id == categoryId).Select(p => p.id).ToList();
            var result = new List<FundraiserProduct>();
            foreach (var id in productsId)
            {
                var product = GetById(id);
                result.Add(product);
            }
            return result;
        }

        public IList<FundraiserProduct> GetAllProducts()
        {
            var productsId = _dataProvider.fundraising_products.ToList().Select(p => p.id);
            var result = new List<FundraiserProduct>();
            foreach (var id in productsId)
            {
                var product = GetById(id);
                result.Add(product);
            }
            return result;
        }



        public FundraiserProduct GetById(int id)
        {
            var fp = _dataProvider.fundraising_products.Find(id);
            var result = FundraiserProductMapper.Hydrate(fp);
            return result;
        }

       public IList<FundraiserProduct> GetAll()
       {
            var productsId = _dataProvider.fundraising_products.ToList().Select(p => p.id);
            var result = new List<FundraiserProduct>();
            foreach (var id in productsId)
            {
                var product = GetById(id);
                result.Add(product);
            }
            return result;
        }

       public int Save(FundraiserProduct model)
       {
          throw new System.NotImplementedException();
       }

       public void Update(FundraiserProduct model)
       {
          throw new System.NotImplementedException();
       }

       public void Delete(FundraiserProduct model)
       {
          throw new System.NotImplementedException();
       }
    }
}
