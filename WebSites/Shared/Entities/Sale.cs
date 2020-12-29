using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
   public class Sale
   {
      public Sale()
      {
         Items = new List<SaleItem>();
         Taxes = new List<AppliedTax>();
      }
      /// <summary>
      /// User Id
      /// </summary>
      public string UserId { get; set; }
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Payment Method 
      /// </summary>
      public PaymentMethod PaymentMethod { get; set; }
      /// <summary>
      /// Internal Payment Method (Types of Credit Cards, Purchase Order, etc)
      /// </summary>
      public InternalPaymentMethod InternalPaymentMethod { get; set; }
      /// <summary>
      /// Client
      /// </summary>
      public Client Client { get; set; }
      /// <summary>
      /// Client Id
      /// </summary>
      public int ClientId { get; set; }
      /// <summary>
      /// Credit Card Information
      /// </summary>
      public CreditCard CreditCard { get; set; }
      /// <summary>
      /// Consultant Id
      /// </summary>
      public int ConsultantId { get; set; }
      /// <summary>
      /// Total Amount
      /// </summary>
      public double TotalAmount { get; set; }
      /// <summary>
      /// Items
      /// </summary>
      public IList<SaleItem> Items { get; set; }
      /// <summary>
      /// Shipping Fee
      /// </summary>
      public double ShippingFee { get; set; }
      /// <summary>
      /// Status
      /// </summary>
      public SaleStatus Status { get; set; }
      /// <summary>
      /// AR Status
      /// </summary>
      public ARStatus ARStatus { get; set; }
      /// <summary>
      /// Confirmation Date
      /// </summary>
      public DateTime? Confirmed { get; set; }
      /// <summary>
      /// Scheduled Delivery Date
      /// </summary>
      public DateTime ScheduledDelivery { get; set; }
      /// <summary>
      /// Taxes
      /// </summary>
      public IList<AppliedTax> Taxes { get; set; }
      /// <summary>
      /// Total amount of discounts applied
      /// </summary>
      public double Discounts { get; set; }
      /// <summary>
      /// Total amount of surchages applied
      /// </summary>
      public double Surcharges { get; set; }
      /// <summary>
      /// Promotion Code (if applicable)
      /// </summary>
      public PromotionCode PromotionCode { get; set; }
      /// <summary>
      /// Promotion Code Id (if aplicable)
      /// </summary>
      public int PromotionCodeId { get; set; }
      /// <summary>
      /// Consultant
      /// </summary>
      public Consultant Consultant { get; set; }
      /// <summary>
      /// Consultant
      /// </summary>
      public string DeliveryComments { get; set; }
    }
}
