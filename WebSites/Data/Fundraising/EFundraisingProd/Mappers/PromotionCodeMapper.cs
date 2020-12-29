using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class PromotionCodeMapper
   {
      public static PromotionCode Hydrate(Promotion_Code promotionCode)
      {
         var model = new PromotionCode
         {
            Id = promotionCode.Promotion_Code_ID,
            Code = promotionCode.Code,
            AmountDiscount = promotionCode.Amount_Discount ?? 0,
            Country = new Country {Code = promotionCode.Country_Code},
            Created = promotionCode.Created,
            MinimumAmount = promotionCode.Minimum_Amount ?? 0.0,
            Name = promotionCode.Promotion_Code_Desc,
            PercentageDiscount = promotionCode.Percentage_Discount ?? 0.0,
            DiscountType = (PromotionCodeDiscountType)promotionCode.Discount_Type,
            IsEnabled = promotionCode.Is_Enabled,
            LimitType = (PromotionCodeLimitType) promotionCode.Limit_Type,
            MinimumQuantity = promotionCode.Minimum_Quantity ?? 0,
            MinimumRequirementType = (PromotionCodeMinimumRequirementType)promotionCode.Minimum_Requirement_Type,
            Description = promotionCode.Description,
            Partner = new Partner { Id = promotionCode.Partner_Id ?? 0},
            PartnerScopeType = (PromotionCodePartnerScopeType)promotionCode.Partner_Scope_Type,
            QuantityLimit = promotionCode.Quantity_Limit ?? 0,
            ScopeType = (PromotionCodeScopeType)promotionCode.Scope_Type,
         };
         if (promotionCode.Date_Limit != null)
         {
            model.DateLimit = promotionCode.Date_Limit;
         }
         return model;
      }

      public static Promotion_Code Dehydrate(PromotionCode model)
      {
         var row =  new Promotion_Code
         {
            Code = model.Code,
            Created = model.Created,
            Partner_Id = model.Partner?.Id,
            Country_Code = model.Country.Code,
            Description = model.Description,
            Is_Enabled = model.IsEnabled,
            Minimum_Amount = model.MinimumAmount,
            Minimum_Quantity = model.MinimumQuantity,
            Limit_Type = (int)model.LimitType,
            Discount_Type = (int)model.DiscountType,
            Partner_Scope_Type = (int)model.PartnerScopeType,
            Percentage_Discount = model.PercentageDiscount,
            Promotion_Code_ID = model.Id,
            Quantity_Limit = model.QuantityLimit,
            Scope_Type = (int)model.ScopeType,
            Promotion_Code_Desc = model.Name,
            Amount_Discount = model.AmountDiscount,
            Minimum_Requirement_Type = (int)model.MinimumRequirementType
         };
         if (model.DateLimit.HasValue)
         {
            row.Date_Limit = new DateTime(model.DateLimit.Value.Year, model.DateLimit.Value.Month, model.DateLimit.Value.Day, 23, 59, 59);
         }
         return row;
      }
   }
}
