using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;


using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;
using log4net;

namespace GA.BDC.Integration.MessageFormatters
{
   internal static class FROrderSharedFormatting
   {
      private static readonly ILog Log = LogManager.GetLogger(typeof(FROrderSharedFormatting));
      public static void FormatFROrderHeaderSegment(sale sale, List<Adjustment> adjustments, Country country, client client, ref ZGA_BDC_ORDER_HDR_SEG FROrderHeaderSegment, lead lead)
      {

         FROrderHeaderSegment.Bdcrepid = sale.consultant_id.ToString().Trim();
         if (lead != null)
         {
            consultant c = Orders.GetConsultantByID(lead.consultant_id);
            if (c != null && c.partner_id == 857)
            {
               FROrderHeaderSegment.Fieldsalesrepid = c != null && c.ext_consultant_id > 0
                  ? c.ext_consultant_id.ToString()
                  : Helper.SAP_BLANK;
            }
            else if (sale.consultant_id > 0)
            {
               c = Orders.GetConsultantByID(sale.consultant_id);
               FROrderHeaderSegment.Fieldsalesrepid = c != null && c.ext_consultant_id > 0 ? c.ext_consultant_id.ToString() : Helper.SAP_BLANK;
            }

            else
            {
               FROrderHeaderSegment.Fieldsalesrepid = Helper.SAP_BLANK.Trim();
            }
         }
         else
         {
            FROrderHeaderSegment.Fieldsalesrepid = Helper.SAP_BLANK.Trim();
         }
         
         var leadId = sale.lead_id;
         if (sale.lead_id != client.lead_id)
         {
            leadId = client.lead_id;
            Log.ErrorFormat("The Lead Id between Client and Sale was different therefore application used the Client Lead Id. Sale Id = {0}. Sales's Lead Id = {3}. Client Id = {1}. Client Sequence Code = {2}. Client's Lead Id = {4}.", sale.sales_id, client.client_id, client.client_sequence_code, sale.lead_id, client.lead_id);
         }
         FROrderHeaderSegment.Leadid = leadId.ToString().Trim();
         FROrderHeaderSegment.Sqlid = FROrderHeaderSegment.Orderrefid = sale.sales_id.ToString().Trim();
         FROrderHeaderSegment.Reqdelondate = sale.scheduled_delivery_date;
         FROrderHeaderSegment.Shipcond = Helper.BULK_ORDER;
         FROrderHeaderSegment.Students = Helper.SAP_BLANK;
         FROrderHeaderSegment.Signdate = sale.confirmed_date;
         FROrderHeaderSegment.Startdate = null;
         FROrderHeaderSegment.Shippingfee = sale.shipping_fees;
         FROrderHeaderSegment.Shipinst = string.IsNullOrEmpty(sale.comment) ? Helper.SAP_BLANK : sale.comment.Trim();
         if (adjustments != null && adjustments.Count > 0)
         {
            FROrderHeaderSegment.Surcharge = adjustments.Where(a => a.Adjustment_No == 2).Select(a => a.Adjustment_On_Sale_Amount).FirstOrDefault();
            FROrderHeaderSegment.Discount = adjustments.Where(a => a.Adjustment_No == 1).Select(a => a.Adjustment_On_Sale_Amount).FirstOrDefault();
         }
         else
         {
            FROrderHeaderSegment.Surcharge = Decimal.Zero;
            FROrderHeaderSegment.Discount = Decimal.Zero;
         }
         FROrderHeaderSegment.Currency = Helper.GetCurrency(country.Country_Code);
         // add logic to distinguish sale from the salesscreen vs online store
         FROrderHeaderSegment.Origin = Helper.IsOnlineOrder(client.client_sequence_code) ? Helper.OnlineOrder : Helper.SalesScreen;

      }

      public static void FormatOrderAccountSegment(client client, client_address clientAddress, string type, bool isShipping, State st, ref ZGA_BDC_ORDER_ACCT_SEG orderAccountSegment)
      {
         orderAccountSegment.Type = type;
         orderAccountSegment.Name1 = (isShipping && !string.IsNullOrEmpty(client.organization)) ? client.organization : client.first_name.Trim() + ' '.ToString() + client.last_name.Trim();
         orderAccountSegment.Name2 = Helper.SAP_BLANK;
         orderAccountSegment.Street1 = string.IsNullOrEmpty(clientAddress.street_address) ? Helper.SAP_BLANK : clientAddress.street_address.Trim();
         orderAccountSegment.Street2 = Helper.SAP_BLANK; 
         orderAccountSegment.City = string.IsNullOrEmpty(clientAddress.city) ? Helper.SAP_BLANK : clientAddress.city.Trim();
         orderAccountSegment.Region = string.IsNullOrEmpty(st.SAP_State_code) ? Helper.SAP_BLANK : st.SAP_State_code.Trim();
         orderAccountSegment.Postcode = string.IsNullOrEmpty(clientAddress.zip_code) ? Helper.SAP_BLANK : clientAddress.country_code.ToUpper() == "CA".ToString() ? Helper.FormatPostalCodeCA(clientAddress.zip_code) : clientAddress.zip_code.Trim();
         orderAccountSegment.Pobox = Helper.SAP_BLANK;
         orderAccountSegment.Poboxpostcode = Helper.SAP_BLANK;
         orderAccountSegment.Country = string.IsNullOrEmpty(clientAddress.country_code) ? Helper.SAP_BLANK : clientAddress.country_code.Trim();
         orderAccountSegment.Email = string.IsNullOrEmpty(client.email) ? Helper.SAP_BLANK : client.email.Trim();
         orderAccountSegment.Dayphone = string.IsNullOrEmpty(client.day_phone) ? Helper.SAP_BLANK : Helper.FormatPhone(client.day_phone.Trim());
         orderAccountSegment.Dayphoneext = string.IsNullOrEmpty(client.day_phone_ext) ? Helper.SAP_BLANK : Helper.FormatPhone(client.day_phone_ext.Trim());
         orderAccountSegment.Evephone = string.IsNullOrEmpty(client.evening_phone) ? Helper.SAP_BLANK : Helper.FormatPhone(client.evening_phone.Trim());
         orderAccountSegment.Mobilephone = string.IsNullOrEmpty(client.other_phone) ? Helper.SAP_BLANK : Helper.FormatPhone(client.other_phone.Trim());
         orderAccountSegment.Fax = string.IsNullOrEmpty(client.fax) ? Helper.SAP_BLANK : client.fax.Trim();
         orderAccountSegment.Faxext = Helper.SAP_BLANK;

      }

      public static void FormatFROrderOrderItemSegment(scratch_book scratchBook, sales_item salesItem, bool freeOfCharge, ref ZGA_BDC_ORDER_ITEM_SEG orderItemSegment)
      {
         if (scratchBook != null && scratchBook.SAPMaterialNo != null)
         {
            orderItemSegment.Materialno = scratchBook.SAPMaterialNo.ToString().Trim();
         }
         else
         {
            orderItemSegment.Materialno = Helper.SAP_BLANK;
         }
         if (freeOfCharge)
         {
            orderItemSegment.Profitpct = Helper.SAP_BLANK;
         }
         else
         {
            if (scratchBook != null)
            {
               orderItemSegment.Profitpct = Payments.GetProfit(salesItem.sales_id, scratchBook.scratch_book_id).ToString();
               if (string.IsNullOrEmpty(orderItemSegment.Profitpct))
               {

                  profit_range pr = Orders.GetPrfofiteRangeBySchratchbookIdAndQuantity(scratchBook.scratch_book_id, Orders.GetSumOfItemsOfSameProductClassWithinSale(salesItem.sales_id, scratchBook.scratch_book_id));
                  if (pr != null)
                  {
                     orderItemSegment.Profitpct = (pr.profit_percentage ?? 0).ToString();
                  }
                  else
                  {
                     throw new Exception("Cannot find profit % for sales item ".ToString() + Environment.NewLine + salesItem.ToString() + Environment.NewLine + scratchBook.ToString());
                  }
               }
            }
            else
            {
               orderItemSegment.Profitpct = Helper.SAP_BLANK;
            }
         }

         orderItemSegment.Pricecode = Helper.IsTaxExluded(true);
         orderItemSegment.Quantity = freeOfCharge ? salesItem.quantity_free : salesItem.quantity_sold;
         orderItemSegment.Unitprice = salesItem.sales_amount;
         orderItemSegment.Uom = null;
         orderItemSegment.Free = freeOfCharge;
         orderItemSegment.Shipsource = Helper.SAP_BLANK;

        }

   }
}
