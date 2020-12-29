using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.Integration.MessageStructures;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;

namespace GA.BDC.Integration.MessageFormatters
{
   internal static class FRPaymentSharedFormatting
   {

      public static void FormatFRPaymentHeaderSegment(payment payment, payment_method paymentMethod,sale sale, Country country, ref ZGA_BDC_PAYMENT_SEG FRPaymentHeaderSegment)
      {
         FRPaymentHeaderSegment.Pmttype = Helper.GetPaymentType(paymentMethod.description);
         FRPaymentHeaderSegment.Leadid = sale.lead_id.ToString();
         FRPaymentHeaderSegment.Orderrefid = payment.sales_id.ToString();
         FRPaymentHeaderSegment.Amount = payment.payment_amount;
         FRPaymentHeaderSegment.Currency = Helper.GetCurrency(country.Country_Code);
         FRPaymentHeaderSegment.Bankdate = payment.cashable_date;
         FRPaymentHeaderSegment.Revind = payment.payment_amount > 0 ? Helper.SAP_FALSE : Helper.SAP_TRUE;
         FRPaymentHeaderSegment.Sqlid = payment.sales_id.ToString() + "-".ToString() + payment.payment_no.ToString();
      }
   }
}
