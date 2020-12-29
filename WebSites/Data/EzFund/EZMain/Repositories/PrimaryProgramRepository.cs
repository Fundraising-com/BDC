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
    class PrimaryProgramRepository : IPrimaryProgramRepository
    {
        private readonly DataProvider _dataProvider;
        public PrimaryProgramRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IList<SitePgmTbl> GetAll()
        {
            const string sql = "SELECT * FROM SITE_PGM_TBL (NOLOCK) WHERE XTRN_END_DTE > GETDATE() and PGM_CDE not in ('PV-3767','SS-SEETOPA','SS-SEETOP','X_SS-LOCKER','SS-LOCKER')";
            var pgmcodes = _dataProvider.Database.Connection.Query<string>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return pgmcodes.Select(GetById).ToList();
        }

        public SitePgmTbl GetById(string code)
        {
            const string sql = "SELECT * FROM SITE_PGM_TBL (NOLOCK) WHERE PGM_CDE = @code";
            var row = _dataProvider.Database.Connection.QueryFirst<site_pgm_tbl>(sql, new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return PrimaryProgramMapper.Hydrate(row);
        }
        public SitePgmTbl GetPrimaryProgramCode(string ppdesc)
        {
            const string sql = "SELECT PGM_CDE FROM SITE_PGM_TBL (NOLOCK) WHERE PGM_DESC_TXT = @ppdesc";
            var row = _dataProvider.Database.Connection.QueryFirst<site_pgm_tbl>(sql, new { ppdesc }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return PrimaryProgramMapper.Hydrate(row);
        }

        public SitePgmTbl GetById(int id)
        {
            throw new NotImplementedException();
        }

        

        public int Save(SitePgmTbl model)
        {
            throw new NotImplementedException();
        }

        public void Update(SitePgmTbl model)
        {
            throw new NotImplementedException();
        }

        public void Delete(SitePgmTbl model)
        {
            throw new NotImplementedException();
        }

        
    }
}
