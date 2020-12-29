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
    class OrganizationTypeRepository : IOrganizationTypeRepository
    {
        private readonly DataProvider _dataProvider;
        public OrganizationTypeRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IList<OrganizationType> GetAll()
        {
            const string sql = "SELECT ORG_TYPE_ID FROM ORG_TYPE_LKUP_TBL (NOLOCK) WHERE ORG_TYPE_ID > 0";
            var grptype = _dataProvider.Database.Connection.Query<int>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return grptype.Select(GetById).ToList();
        }

        public OrganizationType GetById(int id)
        {
            const string sql = "SELECT * FROM ORG_TYPE_LKUP_TBL (NOLOCK) WHERE ORG_TYPE_ID = @id";
            var row = _dataProvider.Database.Connection.QueryFirst<org_type_lkup_tbl>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return OrganizationTypeMapper.HydrateOrganization(row);
        }

        
       

        public int Save(OrganizationType model)
        {
            throw new NotImplementedException();
        }

        public void Update(OrganizationType model)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrganizationType model)
        {
            throw new NotImplementedException();
        }
    }
}
