using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using System;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ArTrnsTblMapper
    {
        public static ar_trns_tbl Dehydrate(ArTrnsTbl payment)
        {
            var result = new ar_trns_tbl
            {
                
                TRNS_TYPE_CDE = "PMT",
                TRNS_DTE = DateTime.Now,
                ORDR_ID = payment.ordrid,
                ORG_ID = payment.orgid,
                OPPOS_ACCT_NBR = null,
                CASH_BATCH_NBR = null,
                PMT_METH_TYPE_CDE = "CCRD",
                PMT_METH_REF_NBR = null,
                TRNS_AMT = payment.trnsamt,
                JE_NBR = null,
                QBKS_POST_DTE = null,
                LAST_MODF_DTE = null,
                LAST_MODF_PRSN_CDE = "EZ_WEB",
                EXT_PAYMENT_ID = null

            };

            return result;

        }
    }
}
