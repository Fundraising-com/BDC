using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("WKFL_PROC_TBL")]
    public partial class wkfl_proc_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int PROC_ID { get; set; }

        [Required]
        public int PROC_TPLT_ID { get; set; }

        public int? PARENT_PROC_ID { get; set; }

        public int? ORG_ID { get; set; }

        public int? CPGN_ID { get; set; }

        public int? ORDR_ID { get; set; }

        public int? STEP_NBR { get; set; }

        public int? MSTONE_CDE { get; set; }

        [StringLength(32)]
        public string STAT_CDE { get; set; }

        public DateTime? STAT_DTE { get; set; }

        [StringLength(16)]
        public string CREA_ACTOR_CDE { get; set; }

        public DateTime? CREA_DTE { get; set; }

        public bool? CMPL_FLG { get; set; }

        public bool? MARK_FOR_HIST_FLG { get; set; }

        public DateTime? LAST_MODF_DTE { get; set; }

        [StringLength(8000)]
        public string PARAM_TXT { get; set; }
    }
}
