namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class special_offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte special_offer_id { get; set; }

        public int brand_id { get; set; }

        public byte product_class_id { get; set; }

        [Required]
        [StringLength(255)]
        public string special_offer_text { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
