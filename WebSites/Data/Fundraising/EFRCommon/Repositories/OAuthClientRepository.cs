using System;
using System.Collections.Generic;
using GA.BDC.Data.Fundraising.EFRCommon.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using Dapper;

namespace GA.BDC.Data.Fundraising.EFRCommon.Repositories
{
   public class OAuthClientRepository : IOAuthClientRepository
   {
      private readonly DataProvider _dataProvider;

      public OAuthClientRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public OAuthClient GetById(int id)
      {
         throw new NotImplementedException();
      }

      public IList<OAuthClient> GetAll()
      {
         throw new NotImplementedException();
      }

      public int Save(OAuthClient model)
      {
         throw new NotImplementedException();
      }

      public void Update(OAuthClient model)
      {
         throw new NotImplementedException();
      }

      public void Delete(OAuthClient model)
      {
         throw new NotImplementedException();
      }

      public OAuthClient GetById(string id)
      {
         const string sql = "SELECT TOP 1 * FROM oauth_client (NOLOCK) WHERE Id = @id";
         var row = _dataProvider.Database.Connection.QueryFirst<oauth_client>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return OAuthClientMapper.Hydrate(row);
      }
   }
}
