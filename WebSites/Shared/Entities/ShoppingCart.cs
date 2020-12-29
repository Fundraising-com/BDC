using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
   public class ShoppingCart
   {
      public ShoppingCart()
      {
         Items = new List<ShoppingCartItem>();
        }
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        public IList<ShoppingCartItem> Items { get; set; }
      /// <summary>
      /// User Id
      /// </summary>
      public string UserId { get; set; }
      /// <summary>
      /// Anonymous Id
      /// </summary>
      public string AnonymousId { get; set; }
      /// <summary>
      /// Status
      /// </summary>
      public ShoppingCartStatus Status { get; set; }
      /// <summary>
      /// Created
      /// </summary>
      public DateTime Created { get; set; }
      /// <summary>
      /// Comments
      /// </summary>
      public string Comments { get; set; }
      /// <summary>
      /// Client Id
      /// </summary>
      public int ClientId { get; set; }
      /// <summary>
      /// Client
      /// </summary>
      public Client Client { get; set; }
      /// <summary>
      /// Promotion Code (if aplicable)
      /// </summary>
      public PromotionCode PromotionCode { get; set; }
      /// <summary>
      /// Promotion Code Id (if aplicable)
      /// </summary>
      public int PromotionCodeId { get; set; }
   }
}
