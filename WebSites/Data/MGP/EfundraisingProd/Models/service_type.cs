namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class service_type
    {
        public service_type()
        {
            sales_item_to_add = new HashSet<sales_item_to_add>();
        }

        [Key]
        public byte service_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<sales_item_to_add> sales_item_to_add { get; set; }
    }
}
