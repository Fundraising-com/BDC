using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PROS_PDCT_TBL")]
    public partial class site_pdct_tbl
    {
        [Key]
        public string PDCT_CDE { get; set; }

        [StringLength(64)]
        public string PDCT_NME { get; set; }

        [Key]
        public string SRC_GRP { get; set; }

        [Key]
        public string PDCT_CTGY_CDE { get; set; }

        public int PDCT_SEQ_NBR { get; set; }

        [StringLength(30)]
        public string PDCT_PRFT_TXT { get; set; }

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

        [StringLength(100)]
        public string URL_TXT { get; set; }

        public bool ITEM_FLAVOR_FLG { get; set; }

        public int ORDR_ITEM_NBR { get; set; }

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

        [StringLength(8000)]
        public string PRODUCT_ORDER_INFO { get; set; }

        [StringLength(8000)]
        public string PRODUCT_EXTRA_INFO { get; set; }
    }
}
