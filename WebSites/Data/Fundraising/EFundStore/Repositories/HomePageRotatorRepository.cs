using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
    public class HomePageRotatorRepository : IHomePageRotatorRepository
    {
        private readonly DataProvider _dataProvider;
        private readonly EFundraisingProd.Tables.DataProvider _efundraisingProdDataProvider;

        public HomePageRotatorRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _efundraisingProdDataProvider = new EFundraisingProd.Tables.DataProvider();
            _efundraisingProdDataProvider.Database.BeginTransaction();
        }

        public IList<HomePageRotator> GetAll()
        {
           
            const string sql = "SELECT Id FROM home_page_rotator (NOLOCK) WHERE is_active = 1 ";
           
            var ids = _dataProvider.Database.Connection.Query<int>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(GetById).ToList();
        }

        public HomePageRotator GetById(int id)
        {
            
            const string sql = "SELECT TOP 1 * FROM home_page_rotator (NOLOCK) WHERE Id = @id";
            var row = _dataProvider.Database.Connection.Query<homepagerotator>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            return HomePageRotatorMapper.Hydrate(row);
        }

        public int Save(HomePageRotator model)
        {
            throw new NotImplementedException();
        }

        public void Update(HomePageRotator model)
        {
            throw new NotImplementedException();
        }
        public void Delete(HomePageRotator model)
        {
            throw new NotImplementedException();
        }

        public IList<HomePageRotator> GetAll(bool active)
        {
            throw new NotImplementedException();
        }
    }
}


