using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("SITE_PGM_TBL")]
    public partial class site_pgm_tbl
    {
        [Key]
        public string PGM_CDE { get; set; }

        [StringLength(64)]
        public string PGM_NME { get; set; }

        [StringLength(80)]
        public string PGM_DESC_TXT { get; set; }

        [StringLength(500)]
        public string EXT_PGM_DESC_TXT { get; set; }

        [StringLength(40)]
        public string PGM_PRFT_TXT { get; set; }

        [StringLength(16)]
        public string PGM_GRP_CDE { get; set; }

        public int PGM_SEQ_NBR { get; set; }

        [StringLength(30)]
        public string MENU_TXT { get; set; }

        [StringLength(80)]
        public string IMAGE_PRFX_NME { get; set; }

        [StringLength(10)]
        public string IMAGE_EXT_NME { get; set; }

        [StringLength(80)]
        public string IMAGE_DESC_TXT { get; set; }

        [StringLength(80)]
        public string SHRT_FEAT_TXT { get; set; }

        [StringLength(4000)]
        public string DESC_TXT { get; set; }

        [StringLength(4000)]
        public string FEAT_TXT { get; set; }

        public int OFRM_PAGE_QTY { get; set; }

        public int XTRN_PAGE_QTY { get; set; }

        public int PDF_FILE_QTY { get; set; }

        public bool PAGE_ORIENT_PORT_FLG { get; set; }

        [StringLength(4000)]
        public string FEAT_PGM_DESC_TXT { get; set; }

        public DateTime XTRN_STRT_DTE { get; set; }

        public DateTime XTRN_END_DTE { get; set; }

        [StringLength(4000)]
        public string META_KYWD_TXT { get; set; }

        [StringLength(4000)]
        public string META_DESC_TXT { get; set; }

        [StringLength(200)]
        public string HTML_TITL_TXT { get; set; }

        [StringLength(80)]
        public string SITE_MAP_DESC_TXT { get; set; }

        [StringLength(200)]
        public string SITE_MAP_URL_TXT { get; set; }

        [StringLength(200)]
        public string CLEAN_URL { get; set; }

        [StringLength(200)]
        public string IMAGE_BANNER { get; set; }
    }
}
