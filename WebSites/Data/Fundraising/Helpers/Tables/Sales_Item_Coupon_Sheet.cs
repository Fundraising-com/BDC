namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Item_Coupon_Sheet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Coupon_Sheet_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Sales_Item_No { get; set; }

        public DateTime Date_Assigned { get; set; }

        public short Sheet_Per_Booklet { get; set; }

        public int? Sponsor_Consultant_ID { get; set; }

        public int? Brand_ID { get; set; }

        public int? Local_Sponsor_ID { get; set; }

        public virtual Coupon_Sheet Coupon_Sheet { get; set; }
    }
}
