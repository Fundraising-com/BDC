using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("SITE_ORG_TYPE_FR_MAP_TBL")]
    public partial class site_org_type_fr_map_tbl
    {

        [Key]
        public int ORG_TYPE_ID { get; set; }

        [StringLength(8)]
        public string LOCL_CDE { get; set; }

        [StringLength(16)]
        public string SRC_GRP { get; set; }

        [StringLength(16)]
        public string GRP_CDE { get; set; }

        public int GRP_SEQ_NBR { get; set; }

        [StringLength(50)]
        public string PGM_CDE { get; set; }

        [StringLength(16)]
        public string PDCT_CDE { get; set; }

        [StringLength(80)]
        public string IMAGE_DESC_TXT { get; set; }

        [StringLength(1000)]
        public string DESC_TXT { get; set; }
    }
}
