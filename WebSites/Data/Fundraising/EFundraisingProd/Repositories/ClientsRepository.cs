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
   public class ClientsRepository : IClientsRepository
   {
      private readonly DataProvider _dataProvider;
      public ClientsRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public IList<Client> GetAll()
      {
         throw new NotImplementedException();
      }

      public int Save(Client client)
      {
         client.Id = _dataProvider.clients.Max(p => p.client_id) + 1;
         var clientToBePersisted = ClientMapper.DehydrateClient(client);
         _dataProvider.clients.Add(clientToBePersisted);
         _dataProvider.SaveChanges();

         var currentAddressId = _dataProvider.client_address.Max(p => p.address_id);
         foreach (var address in client.Addresses)
         {
            address.ClientId = clientToBePersisted.client_id;
            address.ClientSequenceCode = clientToBePersisted.client_sequence_code;
            address.Id = ++currentAddressId;
            var addressToBePersisted = ClientMapper.DehydrateAddress(address);
            _dataProvider.client_address.Add(addressToBePersisted);
            _dataProvider.SaveChanges();
         }

         var activityToBePersisted = new client_activity
         {
            client_id = clientToBePersisted.client_id,
            client_activity_date = DateTime.Now,
            client_activity_type_id = 2,
            client_sequence_code = clientToBePersisted.client_sequence_code,
            comments = "Client purchased.",
            completed_date = DateTime.Now,
            is_contacted = false,
            client_activity_id = _dataProvider.client_activity.Max(p => p.client_activity_id) + 1
         };
         _dataProvider.client_activity.Add(activityToBePersisted);
         _dataProvider.SaveChanges();
         return clientToBePersisted.client_id;
      }

      public void Update(Client model)
      {
         throw new NotImplementedException();
      }

      public void Delete(Client model)
      {
         throw new NotImplementedException();
      }


        //public Client GetByIdAndSequenceCode(int id, string cs)
        //{
        //    var ids =
        //       _dataProvider.Database.Connection.Query<int>(
        //          "SELECT client_id FROM client C (NOLOCK) WHERE C.client_id = @id AND C.client_sequence_code IN (@cs);", new { id, cs },
        //          _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
        //    return GetByIdSalesScreen(ids[0]);
        //}

        public Client GetShippingAddressById(int id)
        {
            const string sql = @"SELECT TOP 1 client_sequence_code, client_id, organization_class_code, group_type_id, channel_code, promotion_id, lead_id, division_id, csr_consultant_id, title_id, salutation, first_name, last_name, title, organization, day_phone, day_time_call, evening_phone, evening_time_call, fax, email, extra_comment, interested_in_agent, interested_in_online, day_phone_ext, evening_phone_ext, other_phone, other_phone_ext
  FROM client C (NOLOCK) WHERE C.client_id = @id;
  SELECT address_id, client_sequence_code, client_id, address_type, street_address, state_code, country_code, city, zip_code, attention_of, matching_code, address_zone_id, phone_1, phone_2, Location, pick_up, warehouse_id
  FROM client_address CA (NOLOCK) WHERE client_id = @id;";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id},
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var clientFound = multi.Read<client>().First();
            var result = ClientMapper.Hydrate(clientFound);
            var addresses = multi.Read<client_address>().ToList();
            foreach (var address in addresses)
            {
                result.Addresses.Add(ClientMapper.HydrateAddress(address));
            }
            return result;
        }




        public Client GetByIdAndSequenceCode(int id , string cs)
        {
            const string sql = @"SELECT TOP 1 client_sequence_code, client_id, organization_class_code, group_type_id, channel_code, promotion_id, lead_id, division_id, csr_consultant_id, title_id, salutation, first_name, last_name, title, organization, day_phone, day_time_call, evening_phone, evening_time_call, fax, email, extra_comment, interested_in_agent, interested_in_online, day_phone_ext, evening_phone_ext, other_phone, other_phone_ext
  FROM client C (NOLOCK) WHERE C.client_id = @id AND C.client_sequence_code = @cs;
  SELECT address_id, client_sequence_code, client_id, address_type, street_address, state_code, country_code, city, zip_code, attention_of, matching_code, address_zone_id, phone_1, phone_2, Location, pick_up, warehouse_id
  FROM client_address CA (NOLOCK) WHERE client_id = @id AND client_sequence_code = @cs;";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id, cs },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var clientFound = multi.Read<client>().First();
            var result = ClientMapper.Hydrate(clientFound);
            var addresses = multi.Read<client_address>().ToList();
            foreach (var address in addresses)
            {
                result.Addresses.Add(ClientMapper.HydrateAddress(address));
            }
            return result;
        }

        public Client GetById(int id)
      {
         const string sql = @"SELECT TOP 1 client_sequence_code, client_id, organization_class_code, group_type_id, channel_code, promotion_id, lead_id, division_id, csr_consultant_id, title_id, salutation, first_name, last_name, title, organization, day_phone, day_time_call, evening_phone, evening_time_call, fax, email, extra_comment, interested_in_agent, interested_in_online, day_phone_ext, evening_phone_ext, other_phone, other_phone_ext
  FROM client C (NOLOCK) WHERE C.client_id = @id;
  SELECT address_id, client_sequence_code, client_id, address_type, street_address, state_code, country_code, city, zip_code, attention_of, matching_code, address_zone_id, phone_1, phone_2, Location, pick_up, warehouse_id
  FROM client_address CA (NOLOCK) WHERE client_id = @id;";
         var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new {id},
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var clientFound = multi.Read<client>().Single();
         var result = ClientMapper.Hydrate(clientFound);
         var addresses = multi.Read<client_address>().ToList();
         foreach (var address in addresses)
         {
            result.Addresses.Add(ClientMapper.HydrateAddress(address));
         }
         return result;
      }
   }
}
