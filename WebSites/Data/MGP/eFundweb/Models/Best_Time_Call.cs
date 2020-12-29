namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Best_Time_Call
    {
        [Key]
        [Column("Best_Time_Call")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Best_Time_Call1 { get; set; }

        [Required]
        [StringLength(100)]
        public string Best_Time_Call_Desc { get; set; }

        [Required]
        [StringLength(100)]
        public string Best_Time_Call_Desc_Fr { get; set; }
    }
}
