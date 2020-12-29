namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounting_class
    {
        public accounting_class()
        {
            accounting_class_shipping_fees = new HashSet<accounting_class_shipping_fees>();
            accounting_period_result = new HashSet<accounting_period_result>();
            packages = new HashSet<package1>();
        }

        [Key]
        public byte accounting_class_id { get; set; }

        public byte carrier_id { get; set; }

        public byte shipping_option_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public byte rank { get; set; }

        public byte? delivery_days { get; set; }

        public virtual carrier carrier { get; set; }

        public virtual carrier_shipping_option carrier_shipping_option { get; set; }

        public virtual ICollection<accounting_class_shipping_fees> accounting_class_shipping_fees { get; set; }

        public virtual ICollection<accounting_period_result> accounting_period_result { get; set; }

        public virtual ICollection<package1> packages { get; set; }
    }
}
