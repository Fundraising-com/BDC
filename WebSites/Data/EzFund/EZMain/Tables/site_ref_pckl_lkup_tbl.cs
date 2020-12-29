using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{

    [Table("SITE_REF_PCKL_LKUP_TBL")]
    public partial class site_ref_pckl_lkup_tbl
    {
        [Key]
        public int ELEM_ID { get; set; }

        [StringLength(16)]
        public string APPL_NME { get; set; }

        [StringLength(16)]
        public string LIST_NME { get; set; }

        [StringLength(16)]
        public string ELEM_CDE { get; set; }

        [StringLength(64)]
        public string ELEM_TXT { get; set; }

        public int ELEM_CDE_NBR { get; set; }

        public int ELEM_SEQ_NBR { get; set; }

        [StringLength(30)]
        public string MENU_TXT { get; set; }

        [StringLength(80)]
        public string IMAGE_NME { get; set; }

        [StringLength(80)]
        public string IMAGE_DESC_TXT { get; set; }

        [StringLength(80)]
        public string SHRT_FEAT_TXT { get; set; }

        [StringLength(4000)]
        public string DESC_TXT { get; set; }

        [StringLength(4000)]
        public string FEAT_TXT { get; set; }

        [StringLength(200)]
        public string URL_TXT { get; set; }

        [StringLength(8)]
        public string LOCL_CDE { get; set; }

        public DateTime? XTRN_STRT_DTE { get; set; }

        public DateTime? XTRN_END_DTE { get; set; }

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
        public string PARENT_CLEAN_URL { get; set; }

        public bool IS_PRODUCT_FLAG { get; set; }
    }
}
