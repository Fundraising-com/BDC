namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_partner_type")]
    public partial class C_tbd_partner_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_type_name { get; set; }

        public DateTime create_date { get; set; }
    }
}
