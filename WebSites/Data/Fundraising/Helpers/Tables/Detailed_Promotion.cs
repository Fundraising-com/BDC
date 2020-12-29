namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detailed_Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Promotion_ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        [StringLength(4)]
        public string Target_Age_Group_Code { get; set; }

        [StringLength(4)]
        public string Target_Gender_Group_Code { get; set; }

        [Required]
        [StringLength(4)]
        public string Target_Group_Code { get; set; }

        public short Promotion_Year { get; set; }

        public short Promotion_Month { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int Quantity_Sent { get; set; }

        public int Call_Goal { get; set; }

        public int Card_Budget { get; set; }
    }
}
