namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("promotion")]
    public partial class promotion
    {
        public promotion()
        {
            partner_promotion = new HashSet<partner_promotion>();
        }

        [Key]
        public int promotion_id { get; set; }

        [Required]
        [StringLength(3)]
        public string promotion_type_code { get; set; }

        public int? promotion_destination_id { get; set; }

        [Required]
        [StringLength(255)]
        public string promotion_name { get; set; }

        [StringLength(255)]
        public string script_name { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(255)]
        public string cookie_content { get; set; }

        [StringLength(255)]
        public string keyword { get; set; }

        public bool? is_displayable { get; set; }

        public virtual ICollection<partner_promotion> partner_promotion { get; set; }

        public virtual promotion_destination promotion_destination { get; set; }

        public virtual promotion_type promotion_type { get; set; }
    }
}
