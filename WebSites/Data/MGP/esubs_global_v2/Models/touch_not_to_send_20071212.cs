namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_not_to_send_20071212
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int touch_id { get; set; }
    }
}
