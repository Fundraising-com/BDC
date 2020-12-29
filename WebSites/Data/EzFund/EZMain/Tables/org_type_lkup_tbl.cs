using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_TYPE_LKUP_TBL")]
    public partial class org_type_lkup_tbl
    {
        [Key]
        public int ORG_TYPE_ID { get; set; }

        [StringLength(40)]
        public string ORG_TYPE_TXT { get; set; }

        public int SEQ_NBR { get; set; }
    }
}
