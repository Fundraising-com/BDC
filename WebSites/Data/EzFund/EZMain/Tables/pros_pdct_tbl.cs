using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("PROS_PDCT_TBL")]
    public partial class pros_pdct_tbl
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        public int SEQ_NBR { get; set; }
        [StringLength(16), Key]
        [Required]
        [Column(Order = 2)]
        public string PDCT_CDE { get; set; }
    }
}
