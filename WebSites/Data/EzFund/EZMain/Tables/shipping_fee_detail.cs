using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("shipping_fee_detail")]
    public partial class shipping_fee_detail
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int shipping_fee_id { get; set; }
        [Required]
        public int minimum_quantity { get; set; }
        [Required]
        public int maximum_quantity { get; set; }
        [Required]
        public double fee { get; set; }
    }
}
