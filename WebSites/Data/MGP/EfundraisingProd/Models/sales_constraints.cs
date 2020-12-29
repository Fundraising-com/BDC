namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sales_constraints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte sales_constraint_id { get; set; }

        public byte product_class_id { get; set; }

        [Required]
        [StringLength(250)]
        public string description { get; set; }

        public bool high_level { get; set; }
    }
}
