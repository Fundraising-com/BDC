namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_promotion_type")]
    public partial class C_tbd_promotion_type
    {
        [Key]
        [StringLength(3)]
        public string promotion_type_code { get; set; }

        [Required]
        [StringLength(255)]
        public string promotion_type_name { get; set; }

        public DateTime create_date { get; set; }
    }
}
