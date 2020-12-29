namespace GA.BDC.Data.MGP.EfundraisingProd.Models
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
            Partner_Lead_Commission = new HashSet<Partner_Lead_Commission>();
        }

        [Key]
        public int Partner_Commission_Range_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int MaxThresholdValue { get; set; }

        public int MinThresholdValue { get; set; }

        public virtual ICollection<Partner_Lead_Commission> Partner_Lead_Commission { get; set; }
    }
}
