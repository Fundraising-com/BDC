using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efundraising.EFundraisingCRM.Fulfillment
{
    public class PaymentInvoice
    {

        public static List<PaymentInvoiceResult> GetNewPayments(DateTime lastRunDate)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetNewPayments(lastRunDate);
        }

        public static PaymentInvoiceResult GetPayment(int paymentId)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetPayment(paymentId);
        }

        public static List<InvoiceResult> GetNewNegativeInvoice(DateTime lastRunDate)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetNewNegativeInvoice(lastRunDate);
        }

    }
}
