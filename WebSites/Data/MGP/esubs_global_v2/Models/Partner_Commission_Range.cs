namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner_Commission_Range
    {
        public Partner_Commission_Range()
        {
            Partner_Activation_Commission = new HashSet<Partner_Activation_Commission>();
            Partner_Sales_Commission = new HashSet<Partner_Sales_Commission>();
        }

        [Key]
        public int Partner_Commission_Range_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int MinThresholdValue { get; set; }

        public int MaxThresholdValue { get; set; }

        public virtual ICollection<Partner_Activation_Commission> Partner_Activation_Commission { get; set; }

        public virtual ICollection<Partner_Sales_Commission> Partner_Sales_Commission { get; set; }
    }
}
