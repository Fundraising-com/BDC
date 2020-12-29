using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PROS_PDCT_LKUP_TBL")]
    public partial class pros_pdct_lkup_tbl
    {
        [Key]
        public string PDCT_CDE { get; set; }

        [StringLength(32)]
        public string PDCT_LABL_TXT { get; set; }

        [StringLength(64)]
        public string PDCT_SHRT_NME { get; set; }

        [StringLength(80)]
        public string PDCT_NME { get; set; }

        [StringLength(32)]
        public string XTRN_PDCT_NME { get; set; }

        public int XTRN_PDCT_SEQ_NBR { get; set; }

        [StringLength(64)]
        public string PDCT_OFRM_NME { get; set; }

        [StringLength(32)]
        public string PDCT_GRP_CDE { get; set; }

        public int PDCT_SEQ_NBR { get; set; }

        public int PRZP_DISC_PCT { get; set; }

        public bool STAX_FLG { get; set; }

        [StringLength(32)]
        public string SAMP_PACK_TYPE_CDE { get; set; }

        [StringLength(16)]
        public string UOM_TXT { get; set; }

        public int AGGR_QTY { get; set; }

        [StringLength(16)]
        public string AGGR_UOM_TXT { get; set; }

        public DateTime? NTRN_STRT_DTE { get; set; }

        public DateTime? NTRN_END_DTE { get; set; }

        public DateTime? XTRN_STRT_DTE { get; set; }

        public DateTime? XTRN_END_DTE { get; set; }

        [StringLength(16)]
        public string DIR_GRP_CDE { get; set; }

        public int DIR_SEQ_NBR { get; set; }

    }
}
