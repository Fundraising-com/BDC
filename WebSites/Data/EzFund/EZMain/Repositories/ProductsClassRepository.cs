
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Dapper;
using Dapper.Contrib.Extensions;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Mappers;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class ProductsClassRepository : IProductClassRepository
    {
        private readonly DataProvider _dataProvider;

        public ProductsClassRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public void Delete(ProductClass model)
        {
            throw new NotImplementedException();
        }

        public IList<ProductClass> GetAll()
        {
            const string sql = "SELECT PDCT_CDE FROM PROS_PDCT_LKUP_TBL AS PDCT (NOLOCK) INNER JOIN PROS_PDCT_GRP_LKUP_TBL AS GRP ON PDCT.PDCT_GRP_CDE = GRP.PDCT_GRP_CDE WHERE XTRN_PDCT_NME !='NULL' AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE) ORDER BY GRP.GRP_SEQ_NBR ASC, PDCT.XTRN_PDCT_SEQ_NBR";
            var codes = _dataProvider.Database.Connection.Query<string>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return codes.Select(GetByCode).ToList();
        }


        public ProductClass GetById(int id)
        {
            throw new NotImplementedException();
        }
        public ProductClass GetByCode(string code)
        {
            const string sql = "SELECT * FROM PROS_PDCT_LKUP_TBL (NOLOCK) WHERE PDCT_CDE = @code";
            var row = _dataProvider.Database.Connection.QueryFirst<pros_pdct_lkup_tbl>(sql, new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ProductsClassMapper.Hydrate(row);
        }
        public int Save(ProductClass model)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductClass model)
        {
            throw new NotImplementedException();
        }
    }
}
