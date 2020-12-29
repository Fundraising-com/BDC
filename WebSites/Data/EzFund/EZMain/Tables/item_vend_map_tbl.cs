using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ITEM_VEND_MAP_TBL")]
    public partial class item_vend_map_tbl
    {
        [Required]
        [StringLength(16)]
        public string ITEM_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string VEND_CDE { get; set; }

        [StringLength(32)]
        public string VEND_ITEM_CDE { get; set; }

        public int ITEM_SEQ_NBR { get; set; }

        public decimal ITEM_COST_AMT { get; set; }       
    }
}
