namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_class_desc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_class_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Column("product_class_desc")]
        [Required]
        [StringLength(50)]
        public string product_class_desc1 { get; set; }

        [StringLength(100)]
        public string min_requirements { get; set; }
    }
}
