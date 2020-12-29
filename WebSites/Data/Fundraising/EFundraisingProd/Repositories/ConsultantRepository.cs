using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data.Repositories;
using Dapper;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
   public class ConsultantRepository : IConsultantRepository
   {
      private readonly DataProvider _dataProvider;
      public ConsultantRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Consultant GetById(int id)
      {
         var consultantFound = _dataProvider.Database.Connection.Query<consultant>("SELECT TOP 1 consultant_id, division_id, client_id, client_sequence_code, department_id, partner_id, consultant_transfer_status_id, territory_id, ext_consultant_id, name, is_agent, is_active, nt_login, phone_extension, email_address, home_phone, work_phone, fax_number, toll_free_phone, mobile_phone, pager_phone, default_proposal_text, csr_consultant, objectives, is_available, password, kit_paid, is_fm, create_date, wfc_id FROM consultant (NOLOCK) WHERE consultant_id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
         return ConsultantMapper.Hydrate(consultantFound);
      }

      public IList<Consultant> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT consultant_id FROM consultant (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public int Save(Consultant model)
      {
         throw new System.NotImplementedException();
      }

      public void Update(Consultant model)
      {
         throw new System.NotImplementedException();
      }

      public void Delete(Consultant model)
      {
         throw new System.NotImplementedException();
      }

      public IList<Consultant> GetAll(string name)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT consultant_id FROM consultant (NOLOCK) WHERE name LIKE @name", new {name = "%" + name + "%"}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }
   }
}
