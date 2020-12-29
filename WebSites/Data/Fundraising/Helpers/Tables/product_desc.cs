namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_desc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short product_desc_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(100)]
        public string product_name { get; set; }

        [Required]
        [StringLength(300)]
        public string product_short_desc { get; set; }

        [Required]
        [StringLength(1000)]
        public string product_long_desc { get; set; }

        [StringLength(25)]
        public string product_small_img { get; set; }

        [StringLength(25)]
        public string product_large_img { get; set; }

        public bool available_online { get; set; }

        public virtual language language { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
