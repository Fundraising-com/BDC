using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Data.Fundraising.FastFundraising.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;

namespace GA.BDC.Data.Fundraising.FastFundraising.Repositories
{
   public class RepresentativeRepository : IRepresentativeRepository
   {
      private readonly DataProvider _dataProvider;
      private readonly EFundraisingProd.Tables.DataProvider _efundraisingProdDataProvider;

      public RepresentativeRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
         _efundraisingProdDataProvider = new EFundraisingProd.Tables.DataProvider();
         _efundraisingProdDataProvider.Database.BeginTransaction();
      }

      public Representative GetById(int id)
      {
         const string sql = @"SELECT TOP 1 id, name, ext_id, email_address, active, login, url, city, state, image_url, phone, esubs_parnter_id, SAPAccountNo, profit_raised FROM FC (NOLOCK) WHERE id = @id;SELECT id, fc_id, created_date, comments, commentor, account FROM fc_testimonial (NOLOCK) WHERE fc_id = @id";
         var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new {id},
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var fc = multi.Read<FC>().First();
         var testimonials = multi.Read<fc_testimonial>();
         var result = RepresentativeMapper.Hydrate(fc, testimonials);
         return result;
      }

      public IList<Representative> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT id FROM FC (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public int Save(Representative model)
      {
         throw new System.NotImplementedException();
      }

      public void Update(Representative model)
      {
         throw new System.NotImplementedException();
      }

      public void Delete(Representative model)
      {
         throw new System.NotImplementedException();
      }

      public Representative GetByRedirect(string redirect)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT id FROM FC (NOLOCK) WHERE login = @redirect", new { redirect }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Any() ? GetById(ids.First()) : null;
      }

      public Representative GetByLead(int leadId)
      {
         const string sqlConsultantId = "SELECT C.consultant_id FROM lead L (NOLOCK) JOIN consultant C (NOLOCK) ON L.consultant_id = C.consultant_id AND L.lead_id = @leadId;";
         const string sql = "SELECT id FROM FC (NOLOCK) WHERE ext_id = @consultantId;";
         var consultantId = _efundraisingProdDataProvider.Database.Connection.QueryFirst<int>(sqlConsultantId, new {leadId}, _efundraisingProdDataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var id = _dataProvider.Database.Connection.QueryFirstOrDefault<int>(sql, new { consultantId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return id > 0 ? GetById(id) : null;
      }
   }
}
