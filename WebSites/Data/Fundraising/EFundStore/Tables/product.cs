namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [Key]
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

        public bool is_featured { get; set; }
    }
}
