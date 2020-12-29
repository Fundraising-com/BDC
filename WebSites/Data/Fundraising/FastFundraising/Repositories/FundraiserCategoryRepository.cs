using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.FastFundraising.Mappers;
using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.FastFundraising.Repositories
{
    public class FundraiserCategoryRepository : IFundraiserCategoryRepository
    {
        private readonly DataProvider _dataProvider;

        public FundraiserCategoryRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public IList<FundraiserCategory> GetAll()
        {
            var categoryIds = _dataProvider.fundraising_categories.ToList().Select(p => p.id);
            var result = new List<FundraiserCategory>();
            foreach (var id in categoryIds)
            {
                var category = GetById(id);
                result.Add(category);
            }
            return result;
        }

       public int Save(FundraiserCategory model)
       {
          throw new System.NotImplementedException();
       }

       public void Update(FundraiserCategory model)
       {
          throw new System.NotImplementedException();
       }

       public void Delete(FundraiserCategory model)
       {
          throw new System.NotImplementedException();
       }

       public FundraiserCategory GetById(int id)
        {
            var fc = _dataProvider.fundraising_categories.Find(id);
            var result = FundraiserCategoryMapper.Hydrate(fc);
            return result;
        }
    }
}
