namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class promotion_type
    {
        public promotion_type()
        {
            promotions = new HashSet<promotion>();
        }

        [Key]
        [StringLength(3)]
        public string promotion_type_code { get; set; }

        [Required]
        [StringLength(255)]
        public string promotion_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<promotion> promotions { get; set; }
    }
}
