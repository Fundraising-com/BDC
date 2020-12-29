namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brand_Coupon_Sheet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Brand_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Coupon_Sheet_ID { get; set; }

        public short? Coupon_Per_Sheet { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Coupon_Sheet Coupon_Sheet { get; set; }
    }
}
