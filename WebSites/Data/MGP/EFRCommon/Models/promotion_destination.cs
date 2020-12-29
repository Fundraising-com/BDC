namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class promotion_destination
    {
        public promotion_destination()
        {
            promotions = new HashSet<promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int promotion_destination_id { get; set; }

        [Required]
        [StringLength(255)]
        public string promotion_destination_url { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<promotion> promotions { get; set; }
    }
}
