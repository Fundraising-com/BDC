using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORDR_ITEM_TBL")]
    public partial class ordr_item_tbl
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ORDR_SUB_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ITEM_CDE { get; set; }

        public int? ITEM_RAW_QTY { get; set; }

        public int? ITEM_EXTRA_QTY { get; set; }

        public int? ITEM_REBT_QTY { get; set; }

        [StringLength(8)]
        public string ITEM_RAW_UOM_TXT { get; set; }

        public int? ITEM_PO_QTY { get; set; }

        [StringLength(8)]
        public string ITEM_PO_UOM_TXT { get; set; }

        public int? ITEM_INVOIC_QTY { get; set; }

        [StringLength(8)]
        public string ITEM_INVOIC_UOM_TXT { get; set; }

        public decimal? ITEM_INVOIC_UNIT_AMT { get; set; }

        public decimal? ITEM_INVOIC_EXT_AMT { get; set; }

        public decimal? ITEM_SLS_UNIT_AMT { get; set; }

        public decimal? ITEM_SLS_EXT_AMT { get; set; }

        [StringLength(64)]
        public string ITEM_CMNT_TXT { get; set; }
    }
}
