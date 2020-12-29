using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_LEAD_STAT_ATRL_TBL")]
    public partial class org_lead_stat_atrl_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ATRL_ID { get; set; }

        [Required]
        public int ORG_ID { get; set; }

        [StringLength(16)]
        public string LEAD_STAT_CDE { get; set; }

        public DateTime ATRL_DTE { get; set; }

        [StringLength(16)]
        public string ATRL_PRSN_CDE { get; set; }
    }
}
