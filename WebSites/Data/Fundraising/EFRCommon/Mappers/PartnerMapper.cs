using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFRCommon.Mappers
{
    public static class PartnerMapper
    {
        public static Partner Hydrate(partner partner)
        {
            var result = new Partner
                {
                    Id = partner.partner_id,
                    Name = partner.partner_name,
                    Guid = partner.guid,
                    HasCollectionSite = partner.has_collection_site,
                    Created = partner.create_date,
                    IsActive = partner.is_active
                };               
            return result;
        }

        public static partner Dehydrate(Partner partner)
        {
            var result = new partner
            {
                partner_id = partner.Id,
                create_date = partner.Created,
                guid = partner.Guid,
                has_collection_site = partner.HasCollectionSite,
                is_active = partner.IsActive,
                partner_name = partner.Name,
                partner_type_id = partner.Type
            };
            return result;
        }
    }
}
