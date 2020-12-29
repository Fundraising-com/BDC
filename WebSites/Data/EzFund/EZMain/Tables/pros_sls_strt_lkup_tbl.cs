using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PROS_SLS_STRT_LKUP_TBL")]
    public partial class pros_sls_strt_lkup_tbl
    {
        [Key, Required]
        public int SLS_STRT_CDE { get; set; }

        [StringLength(16)]
        public string SLS_STRT_TXT_CDE { get; set; }

        public int SLS_STRT_SEQ_NBR { get; set; }

        [StringLength(64)]
        public string SLS_STRT_TXT { get; set; }
    }
}
