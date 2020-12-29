using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_MSTR_TBL")]
    public partial class org_mstr_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ORG_ID { get; set; }

        [Required]
        [StringLength(40)]
        public string ORG_NME { get; set; }

        [Required]
        public int DEPT_ID { get; set; }

        [Required]
        public int ORG_TYPE_ID { get; set; }

        [Required]
        public int ISD_ID { get; set; }

        [StringLength(8)]
        public string LOCL_CDE { get; set; }

        [StringLength(8)]
        public string ZIP_LKUP_CDE { get; set; }

        public int? ORG_MEMB_QTY { get; set; }

        public int? PRIM_ADDR_ID { get; set; }

        public int? PRIM_CTCT_ID { get; set; }

        [StringLength(4)]
        public string PH_1_TYPE_CDE { get; set; }

        [StringLength(25)]
        public string PH_1_NBR { get; set; }

        [StringLength(4)]
        public string PH_2_TYPE_CDE { get; set; }

        [StringLength(25)]
        public string PH_2_NBR { get; set; }

        [StringLength(4)]
        public string PH_3_TYPE_CDE { get; set; }

        [StringLength(25)]
        public string PH_3_NBR { get; set; }

        [StringLength(4)]
        public string FAX_TYPE_CDE { get; set; }

        [StringLength(25)]
        public string FAX_NBR { get; set; }

        [StringLength(64)]
        public string WWW_TXT { get; set; }

        [StringLength(10)]
        public string SLSP_CDE { get; set; }

        [StringLength(10)]
        public string SSPP_CDE { get; set; }

        [StringLength(8)]
        public string LEAD_RTG_CDE { get; set; }

        [StringLength(16)]
        public string LEAD_STAT_CDE { get; set; }

        public DateTime? LEAD_STAT_MODF_DTE { get; set; }

        [StringLength(16)]
        public string LEAD_RFRL_CDE { get; set; }

        public DateTime? LEAD_RFRL_MODF_DTE { get; set; }

        [StringLength(16)]
        public string PMT_TERM_CDE { get; set; }

        [StringLength(20)]
        public string SOLM_ACCT_NBR { get; set; }

        [StringLength(20)]
        public string GM_ACCT_NBR { get; set; }

        [Required]
        public DateTime CREA_DTE { get; set; }

        [StringLength(10)]
        public string CREA_PRSN_CDE { get; set; }

        [Required]
        public DateTime LAST_MODF_DTE { get; set; }

        [StringLength(10)]
        public string LAST_MODF_PRSN_CDE { get; set; }

        [StringLength(40)]
        public string CANON_ORG_NME { get; set; }
    }
}
