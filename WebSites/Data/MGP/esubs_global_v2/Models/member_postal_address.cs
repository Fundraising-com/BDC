namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member_postal_address
    {
        [Key]
        public int member_postal_address_id { get; set; }

        public int member_id { get; set; }

        public int postal_address_id { get; set; }

        public int postal_address_type_id { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }

        public virtual member member { get; set; }

        public virtual postal_address postal_address { get; set; }

        public virtual postal_address_type postal_address_type { get; set; }
    }
}
