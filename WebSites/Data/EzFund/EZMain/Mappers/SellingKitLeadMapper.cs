using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class SellingKitLeadMapper
    {
        public static pros_req_skit_tbl Dehydrate(SellingKitLead sellingkitlead)
        {
            return new pros_req_skit_tbl
            {
                CTCT_NME = sellingkitlead.Name,
                CTCT_TITL_TXT = sellingkitlead.Title,
                ORG_NME = sellingkitlead.Group,
                ADDR_1_TXT = sellingkitlead.Address1 != null ? sellingkitlead.Address1.Address1 : string.Empty,
                ADDR_2_TXT = sellingkitlead.Address2 != null ? sellingkitlead.Address2.Address2 : string.Empty,
                CITY_NME = sellingkitlead.Address1.City != null ? sellingkitlead.Address1.City : string.Empty,
                ST_CDE = sellingkitlead.Address1 != null && sellingkitlead.Address1.Region != null ? sellingkitlead.Address1.Region.Code : string.Empty,
                ZIP_CDE = sellingkitlead.Address1 != null ? sellingkitlead.Address1.PostCode : string.Empty,
                EML_TXT = sellingkitlead.Email,
                PH_1_NBR = sellingkitlead.Phone1,
                PH_2_NBR = null,
                FAX_NBR = null,
                PRIM_PGM_CDE = sellingkitlead.PrimaryProgramCode,
                ORG_TYPE_TXT = sellingkitlead.GroupType,
                ORG_MEMB_QTY = sellingkitlead.NumberOfMembers,
                TARG_PRFT_AMT = sellingkitlead.ProfitAmount,
                SLS_STRT_DTE = sellingkitlead.StartDate,
                CMNT_TXT = sellingkitlead.CommentText,
                SPCL_NOTE_TXT = sellingkitlead.SpecialNoteText,
                SRC_CDE = "EZSKIT",
                RFRL_CDE = sellingkitlead.ReferralCode,
                ORIG_PROS_DTE = DateTime.Now,
                SESS_ID_NBR = sellingkitlead.SessionId,
                RMT_IP_ADDR = sellingkitlead.RemoteIpAddress,
                PROS_STAT_CDE = "UNPROC", 
                LAST_MODF_DTE = null,
                LAST_MODF_PRSN_CDE = null, 
                PROC_MAIL_DTE = null,
                SLSP_RFRL_CDE = null,
                PRZP_REQD_FLG = sellingkitlead.PrizeRequired,
                PRZP_AGE_LEVL_TXT = sellingkitlead.AgeLevel,
                TAG_PGM_CDE = sellingkitlead.TagProgram
                
            };
        }

        //public static pros_pdct_tbl DehydrateProduct(String code)
        //{
        //    return new pros_pdct_tbl
        //    {
        //            PDCT_CDE = code
        //    };
        //}
    }
}
