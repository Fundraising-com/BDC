using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class SaleMapper
   {
      public static sale Dehydrate(Sale sale)
      {

         var result = new sale
         {
            sales_id = sale.Id,
            ar_consultant_id = null,
            consultant_id = sale.ConsultantId,
            carrier_id = 2,
            shipping_option_id = 1,
            payment_term_id = 12,
            client_sequence_code = sale.Client.SequenceCode,
            client_id = sale.Client.Id,
            sales_status_id = (int)sale.Status,
            payment_method_id = (byte)sale.InternalPaymentMethod,
            po_status_id = 1,
            production_status_id = 1,
            sponsor_consultant_id = null,
            ar_status_id = (int)sale.ARStatus,
            lead_id = sale.Client.LeadId,
            billing_company_id = (sale.Client.PromotionId == 5953 || sale.Client.PromotionId == 5961) ? 7 : 1,
            upfront_payment_method_id = null,
            confirmer_id = null,
            collection_status_id = 9,
            confirmation_method_id = 3,
            credit_approval_method_id = 4,
            po_number = "",
            credit_card_no = null,
            expiry_date = sale.CreditCard.ExpirationDate,
            sales_date = DateTime.Now,
            shipping_fees = (decimal)sale.ShippingFee,
            shipping_fees_discount = 0,
            payment_due_date = null,
            scheduled_ship_date = null,
            actual_ship_date = null,
            waybill_no = null,
            comment = sale.Client.Comments,
            coupon_sheet_assigned = false,
            total_amount = (decimal)sale.TotalAmount,
            invoice_date = null,
            cancellation_date = null,
            is_ordered = false,
            po_received_on = null,
            is_delivered = false,
            local_sponsor_found = false,
            box_return_date = null,
            reship_date = null,
            upfront_payment_required = null,
            upfront_payment_due_date = null,
            sponsor_required = false,
            actual_delivery_date = null,
            accounting_comments = null,
            ssn_address = null,
            ssn_city = null,
            ssn_country_code = null,
            ssn_number = null,
            ssn_state_code = null,
            ssn_zip_code = null,
            promised_due_date = null,
            general_flag = false,
            fuelsurcharge = 0,
            is_validated = false,
            cvv2 = string.Empty,
            ext_order_id = 0,            
         };
         if (sale.Confirmed != DateTime.MinValue)
         {
            result.confirmed_date = sale.Confirmed;
         }
         if (sale.ScheduledDelivery != DateTime.MinValue)
         {
            result.scheduled_delivery_date = sale.ScheduledDelivery;
         }
         if (sale.PromotionCodeId > 0)
         {
            result.promotion_code_id = sale.PromotionCodeId;
         }
         return result;
      }

      public static IList<sales_item> DehydrateItem(IList<SaleItem> saleItems)
      {
         var result = new List<sales_item>();
         foreach (var saleItem in saleItems)
         {
            var item = new sales_item
            {
               sales_id = saleItem.SaleId,
               service_type_id = 1,
               sales_item_no = (short)saleItem.Number,
               scratch_book_id = saleItem.ScratchBookId,
               group_name = string.Empty,
               quantity_sold = (short)saleItem.Quantity,
               unit_price_sold = (decimal)saleItem.Product.CalculatedPrice,
               quantity_free = 0,
               suggested_coupons = string.Empty,
               sales_amount = (decimal)(saleItem.Quantity * saleItem.Product.CalculatedPrice),
               paid_amount = 0,
               adjusted_amount = 0,
               discount_amount = 0,
               sponsor_commission_amount = 0,
               sales_commission_amount = 0,
               nb_units_sold = saleItem.Quantity,
               manual_product_description = string.Empty,
            };
            result.Add(item);
         }
         return result;
      }

      public static Sale Hydrate(sale sale, IList<Adjustment> adjustments)
      {
         var result = new Sale
         {
            Id = sale.sales_id,
            TotalAmount = sale.total_amount != null ? (double)sale.total_amount : 0,
            InternalPaymentMethod = (InternalPaymentMethod)sale.payment_method_id,
            Status = (SaleStatus)sale.sales_status_id,
            ARStatus = (ARStatus)sale.ar_status_id,
            PaymentMethod =
               (sale.payment_method_id == 2 || sale.payment_method_id == 3 || sale.payment_method_id == 8 ||
                sale.payment_method_id == 9)
                  ? (PaymentMethod)1
                  : sale.payment_method_id == 16
                     ? (PaymentMethod)2
                     : sale.payment_method_id == 15 ? (PaymentMethod)3 : 0,
            Confirmed = sale.confirmed_date ?? DateTime.MinValue,
            ScheduledDelivery = sale.scheduled_delivery_date ?? DateTime.MinValue,
            ConsultantId = sale.consultant_id,
            ShippingFee = (double)sale.shipping_fees,
            ClientId = sale.client_id,
            PromotionCodeId = sale.promotion_code_id ?? 0
         };
         var discounts = adjustments.Where(p => p.Reason_ID == 9).ToList();
         var surcharges = adjustments.Where(p => p.Reason_ID == 33).ToList();
         result.Discounts = (double)discounts.Sum(x => x.Adjustment_Amount);
         result.Surcharges = (double)surcharges.Sum(x => x.Adjustment_Amount);
         return result;
      }

      public static IList<SaleItem> HydrateItems(IList<sales_item> items)
      {
         var result = new List<SaleItem>();
         foreach (var item in items)
         {
            var saleItem = new SaleItem
            {
               SaleId = item.sales_id,
               Number = item.sales_item_no,
               Quantity = item.quantity_sold,
               ScratchBookId = item.scratch_book_id,
               UnitPrice = (double)item.unit_price_sold,
               SaleAmount = (double)item.sales_amount
            };
            result.Add(saleItem);
         }
         return result;
      }
   }
}
