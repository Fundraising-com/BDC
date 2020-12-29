using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("SITE_TESTML_TBL")]
    public partial class site_testml_tbl
    {
        [Key]
        public int TESTML_ID { get; set; }

        [StringLength(4000)]
        public string TESTML_TXT { get; set; }

        [StringLength(40)]
        public string CTCT_NME { get; set; }
      
        public int ORG_ID { get; set; }
    
        public DateTime? ORG_APVL_DTE { get; set; }
    
        public DateTime? XTRN_STRT_DTE { get; set; }
    
        public DateTime? XTRN_END_DTE { get; set; }
    
        [StringLength(8)]
        public string CREA_PRSN_CDE { get; set; }
      
        public DateTime? CREA_DTE { get; set; }
    }
}
