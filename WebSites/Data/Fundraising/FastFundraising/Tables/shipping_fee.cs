namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shipping_fee
    {
        public shipping_fee()
        {
            shipping_group = new HashSet<shipping_group>();
        }

        [Key]
        public int shipping_fee_id { get; set; }

        public int? sale_amt_min { get; set; }

        public int? sale_amt_max { get; set; }

        [Column("shipping_fee", TypeName = "money")]
        public decimal? shipping_fee1 { get; set; }

        public virtual ICollection<shipping_group> shipping_group { get; set; }
    }
}
