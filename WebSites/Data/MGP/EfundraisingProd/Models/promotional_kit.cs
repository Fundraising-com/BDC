namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class promotional_kit
    {
        [Key]
        public int promotional_kit_id { get; set; }

        public int lead_id { get; set; }

        public int lead_visit_id { get; set; }

        public int kit_type_id { get; set; }

        public byte? carrier_id { get; set; }

        public int? carrier_tracking_id { get; set; }

        public int? postal_address_id { get; set; }

        public short? validated { get; set; }

        public DateTime create_date { get; set; }

        public DateTime? sent_date { get; set; }

        public int? sample_id { get; set; }

        public virtual carrier carrier { get; set; }

        public virtual Kit_Type Kit_Type { get; set; }

        public virtual lead lead { get; set; }

        public virtual Lead_Visit Lead_Visit { get; set; }

        public virtual Sample Sample { get; set; }
    }
}
