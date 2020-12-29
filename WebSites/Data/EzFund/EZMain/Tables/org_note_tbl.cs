using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORG_NOTE_TBL")]
    public partial class org_note_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int NOTE_ID { get; set; }

        [Required]
        public int ORG_ID { get; set; }

        [Required]
        public int CPGN_ID { get; set; }

        [Required]
        public int FKEY_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string NOTE_TYPE_CDE { get; set; }

        [StringLength(1000)]
        public string NOTE_TXT { get; set; }

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
