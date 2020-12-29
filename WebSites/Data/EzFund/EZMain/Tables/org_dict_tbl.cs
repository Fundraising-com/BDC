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
    public partial class org_dict_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ORG_ID { get; set; }

        [StringLength(32)]
        public int DICT_ORG_NME { get; set; }

        [StringLength(32)]
        public int DICT_SET_NME { get; set; }

        [StringLength(32)]
        public int DICT_SGRP_NME { get; set; }

        [StringLength(32)]
        public int DICT_CTCT_NME { get; set; }

        [StringLength(32)]
        public int DICT_MEMB_NME { get; set; }

        [StringLength(10)]
        public int LAST_MODF_PRSN_CDE { get; set; }

        public DateTime LAST_MODF_DTE { get; set; }
    }
}
