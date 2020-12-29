namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_partner_attribute")]
    public partial class C_tbd_partner_attribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_attribute_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_attribute_name { get; set; }

        public DateTime create_date { get; set; }
    }
}
