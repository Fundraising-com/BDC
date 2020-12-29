using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("WKFL_ACTY_TBL")]
    public partial class wkfl_acty_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ACTY_ID { get; set; }

        [Required]
        public int PROC_ID { get; set; }
        
        [Required]
        public int ACTY_TPLT_ID { get; set; }

        public int? PRIO_CDE { get; set; }

        [Required]
        [StringLength(32)]
        public string STAT_CDE { get; set; }

        public DateTime? STAT_DTE { get; set; }

        public DateTime? CREA_DTE { get; set; }

        public DateTime? STRT_DTE { get; set; }

        [Required]
        [StringLength(16)]
        public string ACTOR_GRP_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string ACTOR_CDE { get; set; }

        public bool? CMPL_FLG { get; set; }

        [StringLength(16)]
        public string SPCL_SORT_CDE { get; set; }

        public DateTime? LAST_MODF_DTE { get; set; }

        [StringLength(16)]
        public string LAST_MODF_PRSN_CDE { get; set; }
    }
}
