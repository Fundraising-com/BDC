using System;
using System.Collections.Generic;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;
using GA.BDC.Data.Fundraising.EFRCommon.Mappers;
using GA.BDC.Data.Fundraising.EFRCommon.Tables;

namespace GA.BDC.Data.Fundraising.EFRCommon.Repositories
{
   public class OAuthTokenRepository : IOAuthTokenRepository
   {
      private readonly DataProvider _dataProvider;

      public OAuthTokenRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public OAuthToken GetById(int id)
      {
         throw new NotImplementedException();
      }

      public IList<OAuthToken> GetAll()
      {
         throw new NotImplementedException();
      }

      public int Save(OAuthToken model)
      {
         throw new NotImplementedException();
      }

      bool IOAuthTokenRepository.Save(OAuthToken model)
      {
         var row = OAuthTokenMapper.Dehydrate(model);
         const string sql = "INSERT INTO oauth_token (id, subject, client_id, issued_utc, expires_utc, protected_ticket) VALUES(@id, @subject, @client_id, @issued_utc, @expires_utc, @protected_ticket)";
         var id = _dataProvider.Database.Connection.Execute(sql, new { row.id, row.subject, row.client_id, row.issued_utc, row.expires_utc, row.protected_ticket }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return id > 0;
      }



      public void Update(OAuthToken model)
      {
         throw new NotImplementedException();
      }

      public void Delete(OAuthToken model)
      {
         var row = OAuthTokenMapper.Dehydrate(model);
         const string sql = "DELETE oauth_token WHERE id = @id";
         _dataProvider.Database.Connection.Execute(sql, new { row.id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }

      public OAuthToken GetById(string id)
      {

         const string sql = "SELECT TOP 1 * FROM oauth_token (NOLOCK) WHERE Id = @id";
         var row = _dataProvider.Database.Connection.QueryFirst<oauth_token>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return OAuthTokenMapper.Hydrate(row);
      }
   }
}
