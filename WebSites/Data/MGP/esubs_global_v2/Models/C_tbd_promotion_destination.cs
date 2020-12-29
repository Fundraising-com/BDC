namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_promotion_destination")]
    public partial class C_tbd_promotion_destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int promotion_destination_id { get; set; }

        [Required]
        [StringLength(255)]
        public string promotion_destination_url { get; set; }

        public DateTime create_date { get; set; }
    }
}
