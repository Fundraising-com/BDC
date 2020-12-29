using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;

namespace GA.BDC.Integration.MessageParsers
{
   internal static class FRPaymentConfirmSharedParser
   {
      public static void ParsePaymentConfirmation( sale _sale,  ZGA_BDC_PMT_CONF_SEG paymentSegment, int paymentStatus, ref payment _payment)
      {
         int sqlID;
         try
         {
            sqlID = Convert.ToInt32(paymentSegment.Sqlid);
         }
         catch (Exception)
         {
            try
            {
               sqlID = Convert.ToInt32(Helper.RemoveNonNumeric(paymentSegment.Sqlid));
            }
            catch (Exception exc)
            {
               throw new Exception("Failed to retrive SAPDocNum from ZGA_BDC_PMT_CONF_SEG" + Environment.NewLine + paymentSegment.ToString(), exc.InnerException);
            }

           
         }

         bool refund = false;
         _payment.sales_id = _sale.sales_id;
         if (paymentSegment.Revind == Helper.SAP_TRUE)
         {
            refund = true;
         }

         payment_method pm = Payments.GetPaymentMethodByDesc(Helper.GetSAPPaymentType(paymentSegment.Pmttype));
         if (pm != null)
         {
           _payment.payment_method_id = pm.payment_method_id;
         }
         else
         {
            throw new Exception("Failed to retrive PaymentMehod from ZGA_BDC_PMT_CONF_SEG" + Environment.NewLine + paymentSegment.ToString());
         }

         
       
         _payment.collection_status_id = refund ? Helper.Refund : Helper.Paid;
         _payment.payment_entry_date = _payment.cashable_date = _payment.create_date = DateTime.Now;
         _payment.payment_amount = refund ? (decimal)paymentSegment.Amount * (decimal)-1 : (decimal)paymentSegment.Amount;
         _payment.commission_paid = false;
         _payment.ext_payment_id = sqlID;
         _payment.payment_status_id = paymentStatus;
      
         
      }
      // return whether insert is required vs update
      public static bool ParseAdjustmentConfirmation(sale _sales, ZGA_BDC_PMT_CONF_SEG adjustmentSegment, ref Adjustment _adjustment)
      {

         // 1. Discount 
         // 2. Surcharge
         
         bool debit = (adjustmentSegment.Revind == Helper.SAP_TRUE);
         List<Adjustment> adj = Orders.GetAdjustmentsPerSale(_sales.sales_id);
         if (adj != null && adj.Count > 0)
         {
            if (debit)
            {
               if (adj.Where(a => a.Adjustment_No == 2).Count() > 0)
               {
                  _adjustment = adj.Where(a => a.Adjustment_No == 2).FirstOrDefault();
               }
            }
            {
               if (adj.Where(a => a.Adjustment_No == 1).Count() > 0)
               {
                  _adjustment = adj.Where(a => a.Adjustment_No == 1).FirstOrDefault();
               }
            }
            if (_adjustment != null && _adjustment.Sales_ID > 0)
            {
               if (!string.IsNullOrEmpty(adjustmentSegment.Sqlid))
               {
                  try
                  {
                     _adjustment.Ext_Adjustment_Id = Convert.ToInt32(adjustmentSegment.Sqlid);
                  }
                  catch (Exception)
                  {
                  }
               }
               _adjustment.Reason_ID = Helper.ReasonSAPAdjustment;
               _adjustment.Adjustment_On_Sale_Amount = _adjustment.Adjustment_Amount += (decimal)adjustmentSegment.Amount;
               _adjustment.Create_Date = _adjustment.Adjustment_Date = DateTime.Now;
               return false;
            }
            
         }
         _adjustment.Sales_ID = _sales.sales_id;
         if (!string.IsNullOrEmpty(adjustmentSegment.Sqlid))
         {
            try
            {
               _adjustment.Ext_Adjustment_Id = Convert.ToInt32(adjustmentSegment.Sqlid);
            }
            catch (Exception)
            {
            }
         }
         _adjustment.Adjustment_No = debit ? 2 : 1;
         _adjustment.Reason_ID = Helper.ReasonSAPAdjustment; 
         _adjustment.Adjustment_On_Sale_Amount = _adjustment.Adjustment_Amount = (decimal)adjustmentSegment.Amount;
         _adjustment.Create_Date = _adjustment.Adjustment_Date = DateTime.Now;
         return true;
      }
   }
}
