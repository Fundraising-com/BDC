namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Targeted_Market
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Targeted_Market_ID { get; set; }

        public int Targeted_Market_Type_ID { get; set; }

        public int Advertising_Support_ID { get; set; }

        public int Target_Market_Type_ID { get; set; }

        public bool Seasoner { get; set; }

        [StringLength(25)]
        public string Age_Range { get; set; }

        [StringLength(25)]
        public string Education_Level { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Advertising_Support Advertising_Support { get; set; }

        public virtual targeted_market_type targeted_market_type { get; set; }
    }
}
