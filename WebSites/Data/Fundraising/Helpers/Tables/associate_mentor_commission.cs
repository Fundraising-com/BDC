namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class associate_mentor_commission
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int associate_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mentor_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte product_class_id { get; set; }

        public double? commission_rate { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        public virtual Associate_Mentor Associate_Mentor { get; set; }
    }
}
