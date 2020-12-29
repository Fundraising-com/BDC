using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class LeadMapper
    {
        public static lead Dehydrate(Lead lead)
        {
            return new lead
            {
                CTCT_NME = lead.FirstName + ( (lead.LastName!=null && lead.LastName.Length>0)?(" "+lead.LastName):string.Empty),
                CTCT_TITL_TXT = null,
                ORG_NME = lead.Group,
                ADDR_1_TXT = lead.Address != null ? lead.Address.Address1 : string.Empty,
                ADDR_2_TXT = lead.Address != null ? lead.Address.Address2 : string.Empty, //here we assign the Apt/Suite parameter
                CITY_NME = lead.Address != null ? lead.Address.City : string.Empty,
                ST_CDE = lead.Address != null && lead.Address.Region != null ? lead.Address.Region.Code : string.Empty,
                ZIP_CDE = lead.Address != null ? lead.Address.PostCode : string.Empty,
                EML_TXT = lead.Email,
                PH_1_NBR = lead.Phone,
                PH_2_NBR = null,
                FAX_NBR = null,
                ORG_MEMB_QTY_TXT = lead.NumberOfMembers.ToString(),
                ORG_MEMB_QTY = 0,
                TARG_PRFT_AMT_TXT = lead.AmountToRaise,
                UNIT_SLS_SIZE_TXT = null,
                SLS_STRT_TXT = lead.StartRange,
                SLS_INQ_QTY = 0,
                CMNT_TXT = lead.Comments,
                SRC_CDE = (lead.SourceCode!=null && lead.SourceCode.Length>0)?lead.SourceCode:"EZWEB",
                SRC_SEQ_NBR = 0,
                RFRL_CDE = lead.ReferralCode,
                ORIG_PROS_DTE = DateTime.Now,
                SESS_ID_NBR = "",
                RMT_IP_ADDR = "",
                PROS_STAT_CDE = "UNPROC", 
                LAST_MODF_DTE = null,
                LAST_MODF_PRSN_CDE = null, 
                PROC_MAIL_DTE = null,
                SLSP_RFRL_CDE = null,
                RFRL_URL = lead.ReferralUrl
            };
        }



        public static Lead Hydrate(lead lead)
        {
            var result = new Lead
            {
                Group = lead.ORG_NME,
                FirstName = lead.CTCT_NME,
                Phone = lead.PH_1_NBR,
                Email = lead.EML_TXT,
                SrcSeqNbr = lead.SEQ_NBR,
                Id = lead.SEQ_NBR,
                CtctNme = lead.CTCT_NME,
                OrgNme = lead.ORG_NME,
                Addr1Txt = lead.ADDR_1_TXT,
                CityNme = lead.CITY_NME,
                StCde = lead.ST_CDE,
                ZipCde = lead.ZIP_CDE,
                EmlTxt = lead.EML_TXT,

                Ph1Nbr = lead.PH_1_NBR,
                OrgMembQtyTxt = lead.ORG_MEMB_QTY_TXT,
                OrgMembQty = lead.ORG_MEMB_QTY,
                TargPrftAmtTxt = lead.TARG_PRFT_AMT_TXT,
                SlsStrtTxt = lead.SLS_STRT_TXT,
                CmntTxt = lead.CMNT_TXT,
                GrpType = lead.GRP_TYPE
                

            };

            return result;

        }

        
        public static pros_pdct_tbl DehydrateProduct(String code)
        {
            return new pros_pdct_tbl
            {
                    PDCT_CDE = code
            };
        }
    }
}
