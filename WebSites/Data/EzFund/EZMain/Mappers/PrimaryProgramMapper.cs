using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class PrimaryProgramMapper
    {
        public static SitePgmTbl Hydrate(site_pgm_tbl row)
        {
            return new SitePgmTbl
            {
                pgmcode = row.PGM_CDE,
                pgmname = row.PGM_NME,
                pgmdesctxt = row.PGM_DESC_TXT,
                SITEMAPURLTXT = row.SITE_MAP_URL_TXT,
                EXTPGMDESCTXT = row.EXT_PGM_DESC_TXT,
                PGMPRFTTXT = row.PGM_PRFT_TXT,
                PGMGRPCDE = row.PGM_GRP_CDE,
                PGMSEQNBR = row.PGM_SEQ_NBR,
                MENUTXT = row.MENU_TXT,
                IMAGEPRFXNME = row.IMAGE_PRFX_NME,
                IMAGEEXTNME = row.IMAGE_EXT_NME,
                IMAGEDESCTXT = row.IMAGE_DESC_TXT,
                SHRTFEATTXT = row.SHRT_FEAT_TXT,
                DESCTXT = row.DESC_TXT,
                FEATTXT = row.FEAT_TXT,
                OFRMPAGEQTY = row.OFRM_PAGE_QTY,
                XTRNPAGEQTY = row.XTRN_PAGE_QTY,
                PDFFILEQTY = row.PDF_FILE_QTY,
                PAGEORIENTPORTFLG = row.PAGE_ORIENT_PORT_FLG,
                FEATPGMDESCTXT = row.FEAT_PGM_DESC_TXT,
                XTRNSTRTDTE = row.XTRN_STRT_DTE,
                XTRNENDDTE = row.XTRN_END_DTE,
                METAKYWDTXT = row.META_KYWD_TXT,
                METADESCTXT = row.META_DESC_TXT,
                HTMLTITLTXT = row.HTML_TITL_TXT,
                SITEMAPDESCTXT = row.SITE_MAP_DESC_TXT


    };
        }
    }
}
