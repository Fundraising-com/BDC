namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Targeted_Market
    {
        public Targeted_Market()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Targeted_Market_ID { get; set; }

        public int? Targeted_Market_Type_ID { get; set; }

        public int? Advertising_Support_ID { get; set; }

        public int? Target_Market_Type_ID { get; set; }

        public bool Seasonner { get; set; }

        [StringLength(25)]
        public string Age_Range { get; set; }

        [StringLength(25)]
        public string Education_Level { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }

        public virtual Targeted_Market_Type Targeted_Market_Type { get; set; }
    }
}
