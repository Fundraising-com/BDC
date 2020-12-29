namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_exception_type
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int payment_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int exception_type_id { get; set; }

        public DateTime create_date { get; set; }

        public DateTime? validated_date { get; set; }

        public bool Is_Corrected { get; set; }

        public virtual exception_type exception_type { get; set; }

        public virtual payment payment { get; set; }
    }
}
