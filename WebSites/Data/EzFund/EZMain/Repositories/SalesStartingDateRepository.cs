using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using Dapper;
using Dapper.Contrib.Extensions;
using GA.BDC.Data.EzFund.EZMain.Mappers;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class SalesStartingDateRepository : IStartingDateRepository
    {
        private readonly DataProvider _dataProvider;

        public SalesStartingDateRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(StartingDate model)
        {
            throw new NotImplementedException();
        }

        public IList<StartingDate> GetAll()
        {
            const string sql = "SELECT SLS_STRT_CDE FROM PROS_SLS_STRT_LKUP_TBL (NOLOCK) ORDER BY SLS_STRT_SEQ_NBR ASC";
            var ids = _dataProvider.Database.Connection.Query<int>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(GetById).ToList();
        }

        public StartingDate GetById(int id)
        {
            const string sql = "SELECT * FROM PROS_SLS_STRT_LKUP_TBL (NOLOCK) WHERE SLS_STRT_CDE = @id";
            var row = _dataProvider.Database.Connection.QueryFirst<pros_sls_strt_lkup_tbl>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return SalesStartingDateMapper.Hydrate(row);
        }

        public int Save(StartingDate model)
        {
            throw new NotImplementedException();
        }

        public void Update(StartingDate model)
        {
            throw new NotImplementedException();
        }
    }
}
