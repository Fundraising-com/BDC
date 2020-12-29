namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class campaign_merges
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int original_campaign { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int old_campaign { get; set; }

        [StringLength(50)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime date_changed { get; set; }

        [StringLength(200)]
        public string comment { get; set; }
    }
}
