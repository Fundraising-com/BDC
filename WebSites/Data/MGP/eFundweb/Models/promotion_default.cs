namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class promotion_default
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int promotion_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public DateTime datestamp { get; set; }
    }
}
