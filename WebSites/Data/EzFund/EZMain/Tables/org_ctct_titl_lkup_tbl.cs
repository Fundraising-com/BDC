using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_CTCT_TITL_LKUP_TBL")]
    public partial class org_ctct_titl_lkup_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int CTCT_TITL_ID { get; set; }

        [StringLength(40)]
        public int CTCT_TITL_TXT { get; set; }

        public int SEQ_NBR { get; set; }
    }
}
