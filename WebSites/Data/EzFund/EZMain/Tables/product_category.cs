using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("product_category")]
    public partial class product_category
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int category_id { get; set; }

        public byte? display_order { get; set; }

        public bool display { get; set; }

        public DateTime? create_date { get; set; }
    }
}
