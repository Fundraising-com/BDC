using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_CTCT_TBL")]
    public partial class org_ctct_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int CTCT_ID { get; set; }

        [Required]
        public int ORG_ID { get; set; }

        [Required]
        public int CTCT_SEQ_NBR { get; set; }

        [StringLength(40)]
        public string CTCT_NME { get; set; }

        [Required]
        public int CTCT_TITL_ID { get; set; }

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
	    public string EML_TXT { get; set; }

        [StringLength(500)]
	    public string CTCT_NOTE_TXT { get; set; }

        [Required]
        public DateTime CREA_DTE { get; set; }

        [StringLength(10)]
        public string CREA_PRSN_CDE { get; set; }

        [Required]
        public DateTime LAST_MODF_DTE { get; set; }

        [StringLength(10)]
        public string LAST_MODF_PRSN_CDE { get; set; }
    }
}
