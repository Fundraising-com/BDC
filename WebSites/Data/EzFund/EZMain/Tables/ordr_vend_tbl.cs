using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORDR_VEND_TBL")]
    public partial class ordr_vend_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ORDR_SUB_ID { get; set; }

        [Required]
        public int ORDR_ID { get; set; }

        [StringLength(16)]
        public string PGM_CDE { get; set; }

        [StringLength(16)]
        public string SRC_GRP { get; set; }

        [StringLength(16)]
        public string SRC_CDE { get; set; }

        [StringLength(16)]
        public string OFRM_ASSY_CDE { get; set; }

        [StringLength(16)]
        public string OFRM_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string VEND_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string WHSE_CDE { get; set; }

        [Required]
        [StringLength(16)]
        public string PDCT_CDE { get; set; }

        [StringLength(20)]
        public string EZF_PO_NBR { get; set; }

        public DateTime? PO_DTE { get; set; }

        [StringLength(20)]
        public string PO_CONFO_NBR { get; set; }

        public DateTime? PO_CONFO_DTE { get; set; }

        public DateTime? BILL_RCVD_DTE { get; set; }

        public bool SENT_TO_VEND_FLG { get; set; }

        [Required]
        public bool SHOW_REBT_FLG { get; set; }

        [StringLength(1000)]
        public string DLVY_CMNT_TXT { get; set; }

        public DateTime LAST_MODF_DTE { get; set; }

        [StringLength(10)]
        public string LAST_MODF_PRSN_CDE { get; set; }

        public DateTime LAST_STAT_DTE { get; set; }

        [StringLength(10)]
        public string LAST_STAT_CDE { get; set; }
    }
}
