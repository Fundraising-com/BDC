using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class SiteRefPcklLkupTblMapper
    {
        public static SiteRefPcklLkupTbl Hydrate(site_ref_pckl_lkup_tbl siteRefPcklLkupTbl)
        {
            var result = new SiteRefPcklLkupTbl
            {
                ElemId = siteRefPcklLkupTbl.ELEM_ID,
                ApplNme = siteRefPcklLkupTbl.APPL_NME,
                ListNme = siteRefPcklLkupTbl.LIST_NME,
                ElemCde = siteRefPcklLkupTbl.ELEM_CDE,
                ElemTxt = siteRefPcklLkupTbl.ELEM_TXT,
                ElemCdeNbr = siteRefPcklLkupTbl.ELEM_CDE_NBR,
                ElemSeqNbr = siteRefPcklLkupTbl.ELEM_SEQ_NBR,
                MenuTxt = siteRefPcklLkupTbl.MENU_TXT,
                ImageNme = siteRefPcklLkupTbl.IMAGE_NME,
                ImageDescTxt = siteRefPcklLkupTbl.IMAGE_DESC_TXT,
                ShrtFeatTxt = siteRefPcklLkupTbl.SHRT_FEAT_TXT,
                DescTxt = siteRefPcklLkupTbl.DESC_TXT,
                FeatTxt = siteRefPcklLkupTbl.FEAT_TXT,
                UrlTxt = siteRefPcklLkupTbl.URL_TXT,
                LoclCde = siteRefPcklLkupTbl.LOCL_CDE,
                XtrnStrtDte = siteRefPcklLkupTbl.XTRN_STRT_DTE,
                XtrnEndDte = siteRefPcklLkupTbl.XTRN_END_DTE,
                MetaKywdTxt = siteRefPcklLkupTbl.META_KYWD_TXT,
                MetaDescTxt = siteRefPcklLkupTbl.META_DESC_TXT,
                HtmlTitlTxt = siteRefPcklLkupTbl.HTML_TITL_TXT,
                SiteMapDescTxt = siteRefPcklLkupTbl.SITE_MAP_DESC_TXT,
                SiteMapUrlTxt = siteRefPcklLkupTbl.SITE_MAP_URL_TXT
            };

            return result;

        }

    }
}
