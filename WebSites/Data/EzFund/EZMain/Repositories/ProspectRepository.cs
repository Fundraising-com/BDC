using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data;
using GA.BDC.Data.EzFund.EZMain.Tables;
using Dapper;
using Dapper.Contrib.Extensions;


namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class ProspectRepository : IProspectRepository
    {
        private readonly DataProvider _dataProvider;

        public ProspectRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        
        
        
        void IRepository<ProsNmadTbl>.Delete(ProsNmadTbl model)
        {
            throw new NotImplementedException();
        }

        IList<ProsNmadTbl> IRepository<ProsNmadTbl>.GetAll()
        {
            throw new NotImplementedException();
        }

        ProsNmadTbl IRepository<ProsNmadTbl>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ProsNmadTbl GetProspectById(int externalId)
        {
            const string sql = @"SELECT TOP 1 * FROM PROS_NMAD_TBL (NOLOCK) WHERE SEQ_NBR = @externalId";
            var row = _dataProvider.Database.Connection.QueryFirst<lead>(sql, new { externalId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ProspectMapper.Hydrate(row);
        }

        int IRepository<ProsNmadTbl>.Save(ProsNmadTbl model)
        {
            throw new NotImplementedException();
        }

        void IRepository<ProsNmadTbl>.Update(ProsNmadTbl model)
        {
            throw new NotImplementedException();
        }

        
    }
}
