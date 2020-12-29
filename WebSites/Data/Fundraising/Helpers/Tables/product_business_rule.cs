namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_business_rule
    {
        [Key]
        public int product_business_rule_id { get; set; }

        public int product_class_id { get; set; }

        public int? product_id { get; set; }

        public int? min_order { get; set; }

        public double? free { get; set; }

        public int? average_delivery_time { get; set; }

        public int? package_id { get; set; }

        public virtual product_class product_class { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
