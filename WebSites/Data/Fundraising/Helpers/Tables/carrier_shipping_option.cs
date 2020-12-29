namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class carrier_shipping_option
    {
        public carrier_shipping_option()
        {
            accounting_class = new HashSet<accounting_class>();
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        public byte shipping_option_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<accounting_class> accounting_class { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
