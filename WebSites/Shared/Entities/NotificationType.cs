namespace GA.BDC.Shared.Entities
{
   public enum NotificationType
   {
      /// <summary>
      /// Unknown type
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// A kit has been requested
      /// </summary>
      KitRequest = 1,
      /// <summary>
      /// A new lead has been created (Internal Email)
      /// </summary>
      LeadEntry = 2,
      /// <summary>
      /// Information has been requested to a Rep
      /// </summary>
      InformationRequested = 3,
      /// <summary>
      /// User has asked for his password
      /// </summary>
      ResetPassword = 4,
      /// <summary>
      /// A sale has been confirmed (Email to the Client)
      /// </summary>
      OrderConfirmation = 5,
      /// <summary>
      /// A duplicated lead has been created (Internal Email)
      /// </summary>
      DuplicatedLead = 6,
      /// <summary>
      /// A sale has been created (Internal Email)
      /// </summary>
      SaleCreated = 7,
      /// <summary>
      /// A sale failed to be created (Internal Email)
      /// </summary>
      SaleCreationFailed = 8,
      /// <summary>
      /// Something in the porcess of the sale failed (Internal email)
      /// </summary>
      SaleProcessFailed = 9,
      /// <summary>
      /// A credit card process failed (Internal Email)
      /// </summary>
      CreditCardChargeFailed = 10,
      /// <summary>
      /// A Paypal payment process started (Internal Email)
      /// </summary>
      PaypalPaymentStarted = 11,
      /// <summary>
      /// A potential duplicated lead was created (Internal Email)
      /// </summary>
      PotentialDuplicateLead = 12,
      /// <summary>
      /// A sale has been created (Email to the Client)
      /// </summary>
      OrderCreated = 13,
      /// <summary>
      /// A follow up email to the Client after its Order is confirmed (Email to the Client)
      /// </summary>
      OrderFollowUp = 14,
      /// <summary>
      /// The Sale was Paid (Internal Email)
      /// </summary>
      SalePaid = 15,
      /// <summary>
      /// Product's Review Submitted by a Client (Internal Email)
      /// </summary>
      ProductReviewSubmitted = 16,
        /// <summary>
        ///Send sales screen credit card order processed successfully email to client confirmation
        /// </summary>
      SalesScreenOrderConfirmation = 17,
      /// <summary>
      ///Send Sports Apparel GetStarted (email, name, phone, image)
      /// </summary>
      SportsApparelGetStarted = 18,
      /// <summary>
      ///Send Fund Pass Promo Code
      /// </summary>
      SendFundPassPromoCode = 19,
        /// <summary>
        ///Send apparel sale onhold notification
        /// </summary>
        ApparelSalesScreenSale = 20,
      /// <summary>
      ///Send shipping information to client
      /// </summary>
        SalesScreenSendClientShippingDetails = 21,
       /// <summary>
       ///Brochure Order
       /// </summary>
        SendBrochureOrder = 22,
        SendInBlueTest = 23

    }
}
