using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFRCommon.Mappers;
using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;

namespace GA.BDC.Data.Fundraising.EFRCommon.Repositories
{
   public class PromotionRepository : IPromotionRepository
   {

      private readonly DataProvider _dataProvider;


      public PromotionRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }


      public Promotion GetById(int id)
      {
         const string sql = "SELECT TOP 1 promotion_id, promotion_type_code, promotion_destination_id, promotion_name, script_name, active, create_date, cookie_content, keyword, is_displayable FROM promotion (NOLOCK) WHERE promotion_id = @id; SELECT TOP 1 partner_promotion_id, partner_id, promotion_id, create_date FROM partner_promotion (NOLOCK) WHERE promotion_id = @id;";
         using (var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction))
         {
            return PromotionMapper.Hydrate(multi.Read<promotion>().Single(), multi.Read<partner_promotion>().First().partner_id);
         }
      }

      public IList<Promotion> GetAll()
      {
         throw new NotImplementedException();
      }

      public Promotion GetPromotionId(int partnerId, string abid)
      {
         IList<int> promotionsIds;
         if (string.IsNullOrEmpty(abid))
         {
            promotionsIds =
               _dataProvider.Database.Connection.Query<int>(
                  "SELECT P.promotion_id FROM promotion P (NOLOCK) JOIN partner_promotion PP (NOLOCK) ON P.promotion_id = PP.promotion_id JOIN partner_attribute_value PAV (NOLOCK) ON PP.partner_id = PAV.partner_id WHERE PAV.partner_attribute_id = 12 AND PP.partner_id = @partnerId AND P.script_name = PAV.value",
                  new {partnerId}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return promotionsIds.Any() ? GetById(promotionsIds.First()) : null;

         }
         promotionsIds =
               _dataProvider.Database.Connection.Query<int>(
                  "SELECT P.promotion_id FROM promotion P (NOLOCK) JOIN partner_promotion PP (NOLOCK) ON P.promotion_id = PP.promotion_id JOIN partner_attribute_value PAV (NOLOCK) ON PP.partner_id = PAV.partner_id WHERE PAV.partner_attribute_id = 12 AND PP.partner_id = @partnerId AND P.script_name = @abid",
                  new { partnerId, abid }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return promotionsIds.Any() ? GetById(promotionsIds.First()) : null;
      }

      public int Save(Promotion promotion)
      {
         promotion.CreateDate = DateTime.Now;
         var promotionToBePersisted = PromotionMapper.Dehydrate(promotion);
         _dataProvider.promotions.Add(promotionToBePersisted);
         _dataProvider.SaveChanges();
         var promotionPartner = new partner_promotion { create_date = DateTime.Now, partner_id = promotion.PartnerId, promotion_id = promotionToBePersisted.promotion_id };
         _dataProvider.partner_promotion.Add(promotionPartner);
         _dataProvider.SaveChanges();
         return promotion.Id;
      }

      public void Update(Promotion model)
      {
         throw new NotImplementedException();
      }

      public void Delete(Promotion model)
      {
         throw new NotImplementedException();
      }
   }
}
