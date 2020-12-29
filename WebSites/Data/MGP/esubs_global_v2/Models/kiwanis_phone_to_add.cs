namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class kiwanis_phone_to_add
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string phone_number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int payment_info_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool done { get; set; }
    }
}
