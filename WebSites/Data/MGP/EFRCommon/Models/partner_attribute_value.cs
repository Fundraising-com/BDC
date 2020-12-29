namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_attribute_value
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_attribute_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(255)]
        public string value { get; set; }

        public DateTime create_date { get; set; }

        public virtual partner partner { get; set; }

        public virtual partner_attribute partner_attribute { get; set; }
    }
}
