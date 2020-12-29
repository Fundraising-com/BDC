namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        public product()
        {
            order_detail = new HashSet<order_detail>();
        }

        [Key]
        public int product_id { get; set; }

        public int catalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string product_name { get; set; }

        [StringLength(20)]
        public string product_code { get; set; }

        [StringLength(20)]
        public string remit_code { get; set; }

        public int? status_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        public DateTime create_date { get; set; }

        public virtual catalog catalog { get; set; }

        public virtual ICollection<order_detail> order_detail { get; set; }

        public virtual status status { get; set; }
    }
}
