using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
   public class PromotionCode
   {
      public PromotionCode()
      {
         Products = new List<Product>();
      }
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Code
      /// </summary>
      public string Code { get; set; }
      /// <summary>
      /// Name
      /// </summary>
      public string Name { get; set; }
      /// <summary>
      /// Description
      /// </summary>
      public string Description { get; set; }
      /// <summary>
      /// Created
      /// </summary>
      public DateTime Created { get; set; }
      /// <summary>
      /// Is Enabled
      /// </summary>
      public bool IsEnabled { get; set; }
      /// <summary>
      /// Limit Type
      /// </summary>
      public PromotionCodeLimitType LimitType { get; set; }
      /// <summary>
      /// Date Limit
      /// </summary>
      public DateTime? DateLimit { get; set; }
      /// <summary>
      /// Quantity Limit
      /// </summary>
      public int? QuantityLimit { get; set; }
      /// <summary>
      /// Country Code
      /// </summary>
      public Country Country { get; set; }
      /// <summary>
      /// Scope Type
      /// </summary>
      public PromotionCodeScopeType ScopeType { get; set; }
      /// <summary>
      /// Partner
      /// </summary>
      public Partner Partner { get; set; }
      /// <summary>
      /// Partner Scope Type
      /// </summary>
      public PromotionCodePartnerScopeType PartnerScopeType { get; set; }
      /// <summary>
      /// Discount Type
      /// </summary>
      public PromotionCodeDiscountType DiscountType { get; set; }
      /// <summary>
      /// Amount Discounted
      /// </summary>
      public double? AmountDiscount { get; set; }
      /// <summary>
      /// Percentage Discounted
      /// </summary>
      public double? PercentageDiscount { get; set; }
      /// <summary>
      /// Minimum Requirement Type
      /// </summary>
      public PromotionCodeMinimumRequirementType MinimumRequirementType { get; set; }
      /// <summary>
      /// Minimum Quantity
      /// </summary>
      public int? MinimumQuantity { get; set; }
      /// <summary>
      /// Minimum Amount
      /// </summary>
      public double? MinimumAmount { get; set; }
      /// <summary>
      /// (NOT PERSISTENT) For calculation purposes
      /// </summary>
      public double DiscountedAmount { get; set; }
      /// <summary>
      /// Products
      /// </summary>
      public IList<Product> Products { get; set; }
      
   }
}
