namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_type
    {
        public partner_type()
        {
            partners = new HashSet<partner>();
            partner_type_culture = new HashSet<partner_type_culture>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<partner> partners { get; set; }

        public virtual ICollection<partner_type_culture> partner_type_culture { get; set; }
    }
}
