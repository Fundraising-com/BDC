using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efundraising.EFundraisingCRM.Fulfillment
{
    public class AdjustmentInvoice
    {

        public static List<AdjustmentInvoiceResult> GetNewAdjustments(DateTime lastRunDate)
        {

            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetNewAdjustments(lastRunDate);
        }

    }
}
