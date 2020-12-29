namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public Brand()
        {
            Brand_Coupon_Sheet = new HashSet<Brand_Coupon_Sheet>();
            Local_Sponsor = new HashSet<Local_Sponsor>();
            special_offer = new HashSet<special_offer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Brand_ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Promotion { get; set; }

        public virtual ICollection<Brand_Coupon_Sheet> Brand_Coupon_Sheet { get; set; }

        public virtual ICollection<Local_Sponsor> Local_Sponsor { get; set; }

        public virtual ICollection<special_offer> special_offer { get; set; }
    }
}
