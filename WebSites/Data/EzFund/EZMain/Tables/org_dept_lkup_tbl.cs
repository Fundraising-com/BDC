using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_DEPT_LKUP_TBL")]
    public partial class org_dept_lkup_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int DEPT_ID { get; set; }

        [StringLength(40)]
        public int DEPT_NME { get; set; }

        public int SEQ_NBR { get; set; }
    }
}
