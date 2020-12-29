using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PROS_REQ_SKIT_TBL")]

    public partial class pros_req_skit_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int PROS_SKIT_ID  { get; set; }

        [StringLength(40)]
        public string CTCT_NME { get; set; }

        [StringLength(35)]
        public string CTCT_TITL_TXT { get; set; } 
      
        [StringLength(40)]
        public string ORG_NME { get; set; }

        [StringLength(40)]
        public string ORG_TYPE_TXT { get; set; }
        [StringLength(40)]
        public string ADDR_1_TXT { get; set; }

        [StringLength(40)]
        public string ADDR_2_TXT { get; set; }

        [StringLength(30)]
        public string CITY_NME { get; set; }

        [StringLength(20)]
        public string ST_CDE { get; set; }

        [StringLength(10)]
        public string ZIP_CDE { get; set; }

        [StringLength(60)]
        public string EML_TXT { get; set; }

        [StringLength(25)]
        public string PH_1_NBR { get; set; }

        [StringLength(25)]
        public string PH_2_NBR { get; set; }

        [StringLength(25)]
        public string FAX_NBR { get; set; }

        public int ORG_MEMB_QTY { get; set; }

        public int TARG_PRFT_AMT { get; set; }
        public DateTime? SLS_STRT_DTE { get; set; }

        public bool? PRZP_REQD_FLG { get; set; }

        [StringLength(20)]
        public string PRZP_AGE_LEVL_TXT { get; set; }

        [StringLength(1000)]
        public string SPCL_NOTE_TXT { get; set; }

        [StringLength(16)]
        public string PRIM_PGM_CDE { get; set; }

        [StringLength(16)]
        public string TAG_PGM_CDE { get; set; }

        [StringLength(1000)]
        public string CMNT_TXT { get; set; }

        [StringLength(8)]
        public string SRC_CDE { get; set; }

        [StringLength(16)]
        public string RFRL_CDE { get; set; }

        public DateTime? ORIG_PROS_DTE { get; set; }

        [StringLength(20)]
        public string SESS_ID_NBR { get; set; }

        [StringLength(20)]
        public string RMT_IP_ADDR { get; set; }

        [StringLength(20)]
        public string PROS_STAT_CDE { get; set; }

        public DateTime? LAST_MODF_DTE { get; set; }

        [StringLength(10)]
        public string LAST_MODF_PRSN_CDE { get; set; }
        public DateTime? PROC_MAIL_DTE { get; set; }

        [StringLength(10)]
        public string SLSP_RFRL_CDE { get; set; }



    }
}
