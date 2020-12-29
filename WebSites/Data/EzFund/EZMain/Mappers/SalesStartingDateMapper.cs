using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class SalesStartingDateMapper
    {
        public static StartingDate Hydrate(pros_sls_strt_lkup_tbl row)
        {
            return new StartingDate
            {
                StartCode = row.SLS_STRT_CDE,
                StartCodeTxt = row.SLS_STRT_TXT_CDE,
                StartSequenceNumber = row.SLS_STRT_SEQ_NBR,
                StartMessage = row.SLS_STRT_TXT
            };
        }
    }
}
