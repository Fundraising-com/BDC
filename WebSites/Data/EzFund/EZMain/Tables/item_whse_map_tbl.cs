using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ITEM_WHSE_MAP_TBL")]
    public partial class item_whse_map_tbl
    {
        [Required]
        [StringLength(16)]
        public string ITEM_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string WHSE_CDE { get; set; }

        [StringLength(32)]
        public string WHSE_ITEM_CDE { get; set; }

        [StringLength(64)]
        public string WHSE_ITEM_NME { get; set; }

        public int WHSE_ITEM_SEQ_NBR { get; set; }
    }
}
