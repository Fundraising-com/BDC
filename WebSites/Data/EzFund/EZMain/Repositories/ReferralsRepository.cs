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
    public class ReferralsRepository : IReferralRepository
    {
        private readonly DataProvider _dataProvider;

        public ReferralsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(Referral model)
        {
            throw new NotImplementedException();
        }

        public IList<Referral> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Referral> GetLeadActiveReferrals()
        {
            const string sql = "SELECT RFRL_CDE FROM REF_RFRL_LKUP_TBL (NOLOCK) WHERE (PROS_FLG=1 and ACTV_FLG = 1) ORDER BY RFRL_SEQ_NBR ASC";
            var codes = _dataProvider.Database.Connection.Query<string>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return codes.Select(GetByCode).ToList();
        }

        public Referral GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Referral GetByCode(string code)
        {
            const string sql = "SELECT * FROM REF_RFRL_LKUP_TBL (NOLOCK) WHERE RFRL_CDE = @code";
            var row = _dataProvider.Database.Connection.QueryFirst<ref_rfrl_lkup_tbl>(sql, new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ReferralMapper.Hydrate(row);
        }

        public int Save(Referral model)
        {
            throw new NotImplementedException();
        }

        public void Update(Referral model)
        {
            throw new NotImplementedException();
        }
    }
}
