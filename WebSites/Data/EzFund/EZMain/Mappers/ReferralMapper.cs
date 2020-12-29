using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ReferralMapper
    {
        public static Referral Hydrate(ref_rfrl_lkup_tbl row)
        {
            return new Referral
            {
                ReferralCode = row.RFRL_CDE,
                ReferralSequenceNumber = row.RFRL_SEQ_NBR,
                ReferralTxt = row.RFRL_TXT,
                ReferralLeadFlag = row.PROS_FLG,
                ReferralActiveFlag = row.ACTV_FLG
            };
        }
    }
}
