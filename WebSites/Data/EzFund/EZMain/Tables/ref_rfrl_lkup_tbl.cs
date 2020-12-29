using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("REF_RFRL_LKUP_TBL")]
    public partial class ref_rfrl_lkup_tbl
    {
        [Key, Required]
        public string RFRL_CDE { get; set; }

        public int RFRL_SEQ_NBR { get; set; }

        [StringLength(64)]
        public string RFRL_TXT { get; set; }

        public bool PROS_FLG { get; set; }

        public bool ACTV_FLG { get; set; }     
    }   
}
