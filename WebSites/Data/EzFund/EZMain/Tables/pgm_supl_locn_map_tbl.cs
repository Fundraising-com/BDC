using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PGM_SUPL_LOCN_MAP_TBL")]
    public partial class pgm_supl_locn_map_tbl
    {
        [Key]
        [StringLength(16)]
        public string PGM_CDE { get; set; }

        [Key]
        [StringLength(16)]
        public string PDCT_CDE { get; set; }

        [Key]
        [StringLength(16)]
        public string WHSE_CDE { get; set; }

        [Key]
        [StringLength(16)]
        public string VEND_CDE { get; set; }

        [Key]
        [StringLength(5)]
        public string ORG_LOW_ZIP_CDE { get; set; }

        [StringLength(5)]
        public string ORG_HIGH_ZIP_CDE { get; set; }
      
        public bool PRIM_WHSE_FLG { get; set; }

    }
}
