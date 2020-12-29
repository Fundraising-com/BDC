using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace GA.BDC.Data.DataLayer
{
   public class OnlineOrders:DataLayerBase
   {
      public static IQueryable<es_get_valid_orders_items_by_partner_idResult> GetOnlineOrdersByPartnerId(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int partnerId)
      {
         return esubsGlobalV2DataContext.es_get_valid_orders_items_by_partner_id(partnerId).OrderBy(x => Guid.NewGuid());
      }

      public static IQueryable<es_get_valid_orders_items_by_partner_id_and_date_rangeResult> GetOnlineOrdersByPartnerIdAndDateRange(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int partnerId, DateTime fromDate, DateTime toDate)
      {
         return esubsGlobalV2DataContext.es_get_valid_orders_items_by_partner_id_and_date_range(partnerId, fromDate, toDate).OrderBy(x => Guid.NewGuid());
      }

      public static display_product_type GetDisplayProductTypeByExtProductIdAndStoreId(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int? storeId, int? productType)
      {
         return (from dpt in esubsGlobalV2DataContext.display_product_types 
                 where dpt.store_id == storeId 
                 && dpt.external_product_type_id == productType 
                 select dpt).FirstOrDefault();
      }

      public static ISingleResult<es_get_kickoff_by_partner_idResult> GetKickoffsByPartnerId(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int partnerId, DateTime startDate)
      {
         return esubsGlobalV2DataContext.es_get_kickoff_by_partner_id(partnerId, startDate);
      }

      public static ISingleResult<es_get_auto_created_groups_by_partner_idResult> GetAutoCreateByPartnerId(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int partnerId, DateTime startDate)
      {
         return esubsGlobalV2DataContext.es_get_auto_created_groups_by_partner_id(partnerId, startDate);
      }

      public static List<es_get_valid_orders_itemsResult> GetOnlineOrders(EsubsGlobalV2DataContext esubsGlobalV2DataContext)
      {
         const string key = "ValidOnlineOrders";
         var orders = Get<List<es_get_valid_orders_itemsResult>>(key);
         if (orders == null)
         {
            orders = esubsGlobalV2DataContext.es_get_valid_orders_items().ToList();
            Add(orders, key);
         }
         return orders;
      }
      public static List<es_get_valid_orders_items_by_order_item_idResult> GetOnlineOrdersByOrderItemId(EsubsGlobalV2DataContext esubsGlobalV2DataContext, int? orderItemId)
      {
          var orders = Get<List<es_get_valid_orders_items_by_order_item_idResult>>("ValidOnlineOrdersByOrderItemId");
          if (orders == null)
          {
              orders = esubsGlobalV2DataContext.es_get_valid_orders_items_by_order_item_id(orderItemId).ToList();
              Add(orders, "ValidOnlineOrdersByOrderItemId");
          }
          return orders;
      }
   }
}
