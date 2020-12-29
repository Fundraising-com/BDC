using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.Integration.MessageStructures;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;

namespace GA.BDC.Integration.MessageFormatters
{
    internal static class EZPaymentSharedFormatting
    {
        public static void FormatEZPaymentHeaderSegment(AR_TRNS_TBL payment, AR_TRNS_TBL paymentMethod, ORDR_INVOIC_TO_PROCESS_TBL sale, string country, ref ZGA_EZFUND_PAYMENT_SEG EZPaymentHeaderSegment, ORG_CTCT_TBL client)
        {
            EZPaymentHeaderSegment.Bankdate = payment.TRNS_DTE.Date;
            EZPaymentHeaderSegment.Pmttype = Helper.EZGetPaymentType(paymentMethod.PMT_METH_TYPE_CDE);
            EZPaymentHeaderSegment.Leadid = client.CTCT_ID.ToString();
            EZPaymentHeaderSegment.Orderrefid = payment.ORDR_ID.ToString();
            EZPaymentHeaderSegment.Amount = payment.TRNS_AMT;
            EZPaymentHeaderSegment.Currency = Helper.GetCurrency(country);
            EZPaymentHeaderSegment.Revind = payment.TRNS_AMT > 0 ? Helper.SAP_FALSE : Helper.SAP_TRUE;
            EZPaymentHeaderSegment.Sqlid = payment.TRNS_ID.ToString() + "-".ToString() + 1;
        }


    }
}
