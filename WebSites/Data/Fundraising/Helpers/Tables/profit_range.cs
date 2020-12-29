namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class profit_range
    {
        public profit_range()
        {
            product_business_rule_profit_range = new HashSet<product_business_rule_profit_range>();
        }

        [Key]
        public int profit_range_id { get; set; }

        public int? item_nbr_min { get; set; }

        public int? item_nbr_max { get; set; }

        public double? profit_percentage { get; set; }

        public virtual ICollection<product_business_rule_profit_range> product_business_rule_profit_range { get; set; }
    }
}
