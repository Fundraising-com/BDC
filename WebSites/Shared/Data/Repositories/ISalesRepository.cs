using System.Collections.Generic;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface ISalesRepository : IRepository<Sale>
   {

        IList<Sale> GetSaleShippingDetails();
        /// <summary>
        /// Returns a collection of Sales shipping details that meet the business rule to send the Client a Follow Up Notification
        /// </summary>
        /// <returns></returns>
        ///

        IList<Sale> GetByClientId(int clientId);
        /// <summary>
        /// Returns a collection of Sales that meet the business rule to send the Client a Follow Up Notification
        /// </summary>
        /// <returns></returns>
        ///
        IList<Sale> GetByForSSClientId(int clientId);
        /// <summary>
        /// Returns a collection of Sales for SS sports apparel 
        /// </summary>
        /// <returns></returns>
        IList<Sale> GetRequiredFollowUpNotification(DateTime d);
     
      /// <summary>
      /// Returns a collection of Sales placed with a Promotion Code
      /// </summary>
      /// <param name="promotionCodeId"></param>
      /// <returns></returns>
      IList<Sale> GetByPromotionCodeUsed(int promotionCodeId);

      IList<Sale> GetPaidSales(DateTime limitDate);
      int Save(EzFundSale sale);
      EzFundSale GetEzFundSaleByOrderId(int orderId);
      void SaveVendorAndItems(IList<EzFundSaleVendor> vendors, int saleId);
		bool UpdateFundPaymentReferenceByOrderId(int reference, string authNumber);

     void Update(EzFundSale sale);

    }
}
