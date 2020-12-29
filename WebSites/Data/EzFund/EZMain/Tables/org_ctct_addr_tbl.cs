using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_CTCT_ADDR_TBL")]
    public partial class org_ctct_addr_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ADDR_ID { get; set; }

        [Required]
        public int CTCT_ID { get; set; }

        [Required]
        public int ORG_ID { get; set; }

        [StringLength(4)]
        public string ADDR_TYPE_CDE { get; set; }

        [StringLength(40)]
        public string ADDR_1_TXT { get; set; }

        [StringLength(40)]
        public string ADDR_2_TXT { get; set; }

        [StringLength(40)]
        public string ADDR_3_TXT { get; set; }

        [StringLength(30)]
        public string CITY_NME { get; set; }

        [StringLength(10)]
        public string ST_CDE { get; set; }

        [StringLength(10)]
        public string ZIP_CDE { get; set; }

        [StringLength(20)]
        public string CTRY_NME { get; set; }

        [StringLength(500)]
        public string ADDR_NOTE_TXT { get; set; }

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
