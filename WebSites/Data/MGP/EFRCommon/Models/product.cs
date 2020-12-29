namespace GA.BDC.Data.MGP.EFRCommon.Models
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
            product_desc = new HashSet<product_desc>();
            product_package = new HashSet<product_package>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        public int? parent_product_id { get; set; }

        public int scratch_book_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public decimal? raising_potential { get; set; }

        [Required]
        [StringLength(20)]
        public string product_code { get; set; }

        public bool enabled { get; set; }

        public DateTime? create_date { get; set; }

        public bool? is_inner { get; set; }

        public virtual ICollection<product_desc> product_desc { get; set; }

        public virtual ICollection<product_package> product_package { get; set; }
    }
}
