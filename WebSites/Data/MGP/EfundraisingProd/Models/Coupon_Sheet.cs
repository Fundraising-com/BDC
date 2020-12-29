namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupon_Sheet
    {
        public Coupon_Sheet()
        {
            Brand_Coupon_Sheet = new HashSet<Brand_Coupon_Sheet>();
            Sales_Item_Coupon_Sheet = new HashSet<Sales_Item_Coupon_Sheet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Coupon_Sheet_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Product_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public short? Sheet_Per_Booklet { get; set; }

        public DateTime? Expiration_Date { get; set; }

        public bool? Commission_Payable { get; set; }

        public bool Is_Active { get; set; }

        public virtual ICollection<Brand_Coupon_Sheet> Brand_Coupon_Sheet { get; set; }

        public virtual ICollection<Sales_Item_Coupon_Sheet> Sales_Item_Coupon_Sheet { get; set; }
    }
}
