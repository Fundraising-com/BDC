using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("SITE_PDCT_PGM_MAP_TBL")]
    public partial class site_pdct_pgm_map_tbl
    {
        [Key]
        public string PDCT_CTGY_CDE { get; set; }

        [Key]
        public string PGM_CDE { get; set; }

        [Key]
        public string SRC_CDE { get; set; }

        public int SRC_SEQ_NBR { get; set; }

        public int PGM_SEQ_NBR { get; set; }
    }
}
