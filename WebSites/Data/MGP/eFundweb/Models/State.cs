namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Key]
        [StringLength(50)]
        public string State_Name { get; set; }

        public int Avg_Delivery_Days { get; set; }

        public int Time_Zone_Difference { get; set; }
    }
}
