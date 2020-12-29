namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("partner")]
    public partial class partner
    {
        public partner()
        {
            partner_attribute_value = new HashSet<partner_attribute_value>();
            partner_promotion = new HashSet<partner_promotion>();
        }

        [Key]
        public int partner_id { get; set; }

        public int partner_type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string partner_name { get; set; }

        public bool has_collection_site { get; set; }

        public Guid guid { get; set; }

        public DateTime create_date { get; set; }

        public bool is_active { get; set; }

        public virtual ICollection<partner_attribute_value> partner_attribute_value { get; set; }

        public virtual partner_type partner_type { get; set; }

        public virtual ICollection<partner_promotion> partner_promotion { get; set; }
    }
}
