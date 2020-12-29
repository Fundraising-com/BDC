using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.Integration.MessageStructures;

namespace GA.BDC.Integration.MessageParsers
{
    internal static class EZPaymentConfirmSharedParser
    {
        public static void ParsePaymentConfirmation(ORDR_INVOIC_TO_PROCESS_TBL _sale, ZGA_EZFUND_PMT_CONF_SEG paymentSegment, int paymentStatus, ref AR_TRNS_TBL _payment)
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
                    throw new Exception("Failed to retrive SAPDocNum from ZGA_EZFUND_PMT_CONF_SEG" + Environment.NewLine + paymentSegment.ToString(), exc.InnerException);
                }


            }

          
            _payment.ORDR_ID = _sale.ORDR_ID;
            _payment.ORG_ID = (int)_sale.ORG_ID;
            _payment.TRNS_DTE = DateTime.Now;
            _payment.TRNS_AMT =  (decimal)paymentSegment.Amount;
            _payment.PMT_METH_TYPE_CDE = string.IsNullOrEmpty(Helper.ConvertSAPToEZGetPaymentType(paymentSegment.Pmttype)) ? null : Helper.ConvertSAPToEZGetPaymentType(paymentSegment.Pmttype);
            _payment.EXT_PAYMENT_ID = sqlID;
            _payment.PAYMENT_STATUS_ID = paymentStatus;
            _payment.TRNS_TYPE_CDE = "PMT";
           

        }
        // return whether insert is required vs update
        public static bool ParseAdjustmentConfirmation(sale _sales, ZGA_EZFUND_PMT_CONF_SEG adjustmentSegment, ref Adjustment _adjustment)
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
