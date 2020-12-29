using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class HomePageRotatorRepository : IHomePageRotatorRepository
    {
        private readonly DataProvider _dataProvider;

        public HomePageRotatorRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public void Delete(HomePageRotator model)
        {
            throw new NotImplementedException();
        }

        public IList<HomePageRotator> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<HomePageRotator> GetAll(bool active)
        {
            const string sql = "SELECT ID FROM home_page_rotator (NOLOCK) WHERE IS_ACTIVE=@active";

            var ids = _dataProvider.Database.Connection.Query<int>(sql,new { active }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(GetById).ToList();
        }

        public HomePageRotator GetById(int id)
        {
            const string sql = "SELECT TOP 1 * FROM home_page_rotator (NOLOCK) WHERE Id = @id";
            var row = _dataProvider.Database.Connection.Query<home_page_rotator>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
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
    }
}
