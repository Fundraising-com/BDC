using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("SITE_PDCT_TAB_INDEX_TBL")]
    public partial class site_pdct_tab_index_tbl
    {
        [Key]
        public int TAB_ID { get; set; }

        [StringLength(16)]  
        public string PDCT_CDE { get; set; }

        public int TAB_SEQ_NBR { get; set; }
    
        [StringLength(40)]
        public string LABEL_TXT { get; set; }

        [StringLength(4000)]      
        public string DESC_TXT { get; set; }

        public int ORDR_ITEM_NBR { get; set; }
    }
}
