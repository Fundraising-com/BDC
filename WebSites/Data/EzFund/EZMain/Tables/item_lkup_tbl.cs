using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ITEM_LKUP_TBL")]
    public partial class item_lkup_tbl
    {
        [Key]
        public string ITEM_CDE { get; set; }

        [StringLength(64)]
        public string ITEM_NME { get; set; }

        [StringLength(16)]
        public string PDCT_CDE { get; set; }

        [StringLength(32)]
        public string FLVR_NME { get; set; }

        [StringLength(16)]
        public string SIZE_TXT { get; set; }

        [StringLength(16)]
        public string UOM_TXT { get; set; }

        [StringLength(4)]
        public string ITEM_TYPE_CDE { get; set; }

        public bool STAX_FLG { get; set; }

        public decimal? ITEM_INVOIC_AMT { get; set; }

        public int ITEM_AGGR_SIZE_QTY { get; set; }

        [StringLength(16)]
        public string ITEM_AGGR_UOM_TXT { get; set; }

        [StringLength(16)]
        public string SKU_CDE { get; set; }

        public int ITEM_SEQ_NBR { get; set; }

        public DateTime NTRN_STRT_DTE { get; set; }

        public DateTime NTRN_END_DTE { get; set; }

        public bool STAX_INCL_FLG { get; set; }

        public int SAPMaterialNo { get; set; }

        public int PARENT_ID { get; set; }

        public int? PDCT_ITEM_QTY { get; set; }

        public decimal? PDCT_SUGG_PRICE { get; set; }
    }
}
