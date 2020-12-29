using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
   public class OrganizationTypeRepository : IOrganizationTypeRepository
   {
      private readonly DataProvider _dataProvider;
      public OrganizationTypeRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }
      public OrganizationType GetById(int id)
      {
         var entity = _dataProvider.Database.Connection.QueryFirst<organization_type>("SELECT * FROM organization_type (NOLOCK) WHERE organization_type_id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return OrganizationTypeMapper.Hydrate(entity);
      }

      public IList<OrganizationType> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT organization_type_id FROM organization_type (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
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
