using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFRCommon.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Entities;
using Dapper;
namespace GA.BDC.Data.Fundraising.EFRCommon.Repositories
{
   public class PartnerRepository : IPartnerRepository
   {
      private readonly DataProvider _dataProvider;
      private const int defaultPartnerId = 686;

      public PartnerRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Partner GetById(int id)
      {
         const string sql = @"SELECT TOP 1 partner_id, partner_type_id, partner_name, has_collection_site, guid, create_date, is_active FROM partner (NOLOCK) WHERE partner_id = @id; 
SELECT PA.partner_attribute_name as [key], PAV.value as value from partner_attribute PA (NOLOCK) JOIN partner_attribute_value PAV (NOLOCK) ON PA.partner_attribute_id = PAV.partner_attribute_id WHERE PAV.culture_code = 'en-US' AND PAV.partner_id = @id";
         using (var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id },_dataProvider.Database.CurrentTransaction.UnderlyingTransaction))
         {
            var partner = PartnerMapper.Hydrate(multi.Read<partner>().Single());
            partner.Attributes = multi.Read().ToDictionary(p => (string)p.key, p => (string)p.value);
            if (!partner.Attributes.ContainsKey("FR_partner_banner_image"))
            {
               partner.Attributes.Add("FR_partner_banner_image", "686.png");
            }
            if (id != defaultPartnerId)
            {
               using (var multiDefault = _dataProvider.Database.Connection.QueryMultiple(sql, new { id = defaultPartnerId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction))
               {
                  var defaultPartner = PartnerMapper.Hydrate(multiDefault.Read<partner>().Single());
                  defaultPartner.Attributes = multiDefault.Read().ToDictionary(p => (string)p.key, p => (string)p.value);
                  #region Add Default Values in case they don't exist
                  if (!partner.Attributes.ContainsKey("FR_partner_banner_image"))
                  {
                     partner.Attributes.Add("FR_partner_banner_image", defaultPartner.Attributes["FR_partner_banner_image"]);
                  }
                  if (!partner.Attributes.ContainsKey("phone_number"))
                  {
                     partner.Attributes.Add("phone_number", defaultPartner.Attributes["phone_number"]);
                  }
                  if (!partner.Attributes.ContainsKey("trad_url"))
                  {
                     partner.Attributes.Add("trad_url", defaultPartner.Attributes["trad_url"]);
                  }
                  #endregion
               }
            }
            return partner;

         }
      }

      public int Save(Partner partner)
      {
         throw new NotImplementedException();
      }

      public void Update(Partner model)
      {
         throw new NotImplementedException();
      }

      public IList<Partner> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT partner_id FROM partner (NOLOCK);", null,
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public void Delete(Partner partner)
      {
         throw new NotImplementedException();
      }


      public Partner GetByAAId(string aaid)
      {
         var ids =
            _dataProvider.Database.Connection.Query<int>(
               "SELECT TOP 1 PAV.partner_id FROM partner_attribute PA (NOLOCK) JOIN partner_attribute_value PAV (NOLOCK) ON PA.partner_attribute_id = PAV.partner_attribute_id WHERE PAV.culture_code = 'en-US' AND PA.partner_attribute_name = 'pap_a_aid' AND PAV.value = @aaid",
               new { aaid }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Any() ? GetById(ids[0]) : null;
      }
   }
}
