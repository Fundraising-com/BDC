namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
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
            product_business_rule_shipping_fee = new HashSet<product_business_rule_shipping_fee>();
        }

        [Key]
        public int shipping_fee_id { get; set; }

        public int? sale_amt_min { get; set; }

        public int? sale_amt_max { get; set; }

        [Column("shipping_fee", TypeName = "money")]
        public decimal? shipping_fee1 { get; set; }

        public virtual ICollection<product_business_rule_shipping_fee> product_business_rule_shipping_fee { get; set; }
    }
}
