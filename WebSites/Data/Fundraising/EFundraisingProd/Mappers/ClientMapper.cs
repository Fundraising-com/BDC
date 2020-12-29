using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
    public static class ClientMapper
    {
        public static client DehydrateClient(Client client)
        {
            var result = new client
            {
             channel_code   = client.ChannelCode,
             client_sequence_code = client.SequenceCode,
             csr_consultant_id = client.ConsultantId,
             day_phone = client.Phone,
             evening_phone = client.Phone,
             email = client.Email,
             lead_id = client.LeadId,
             first_name = client.FirstName,
             last_name = client.LastName,
             organization = client.Organization,
             promotion_id = client.PromotionId,
             other_phone = client.Phone,
             extra_comment = client.Comments,
             organization_class_code = client.OrganizationClassCode,
             salutation = client.Salutation,
             division_id = (byte) client.DivisionId,
             interested_in_online = false,
             interested_in_agent = false,
             client_id = client.Id
            };
            return result;
        }

        public static client_address DehydrateAddress(ClientAddress clientAddress)
        {
            var result = new client_address
            {
                address_type = clientAddress.Type,
                address_zone_id = clientAddress.AddressZoneId,
                attention_of = clientAddress.AttentionOf,
                client_id = clientAddress.ClientId,
                client_sequence_code = clientAddress.ClientSequenceCode,
                country_code = clientAddress.Country.Code,
                pick_up = false,
                state_code = clientAddress.Region.Code,
                street_address = clientAddress.Address1,
                zip_code = clientAddress.PostCode,
                city = clientAddress.City,
                matching_code = "",//TODO: Javi, what's this?
                address_id = clientAddress.Id
            };
            return result;
        }

       public static ClientAddress HydrateAddress(client_address clientAddress)
       {
          var result = new ClientAddress
          {
             Id = clientAddress.address_id,
             Address1 = clientAddress.street_address,
             Type = clientAddress.address_type,
             AddressZoneId = clientAddress.address_zone_id,
             AttentionOf = clientAddress.attention_of,
             ClientId = clientAddress.client_id,
             ClientSequenceCode = clientAddress.client_sequence_code,
             Country = new Country { Code = clientAddress.country_code},
             Region = new Region { Code = clientAddress.state_code},
             PostCode = clientAddress.zip_code,
             City = clientAddress.city
          };
          return result;
       }

        public static Client Hydrate(client client)
        {
            var result = new Client
            {
                Id = client.client_id,
                ChannelCode = client.channel_code,
                SequenceCode = client.client_sequence_code,
                ConsultantId = client.csr_consultant_id ?? 0,
                Phone = client.day_phone,
                Email = client.email,
                LeadId = client.lead_id ?? 0,
                FirstName = client.first_name,
                LastName = client.last_name,
                Organization = client.organization,
                PromotionId = client.promotion_id,
                Comments = client.extra_comment,
                OrganizationClassCode = client.organization_class_code,
                Salutation = client.salutation,
                DivisionId = client.division_id
            };
            return result;
        }
    }
}