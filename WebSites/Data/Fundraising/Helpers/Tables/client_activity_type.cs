namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_activity_type
    {
        public client_activity_type()
        {
            client_activity = new HashSet<client_activity>();
        }

        [Key]
        public byte client_activity_type_id { get; set; }

        public byte? carrier_shipping_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual carrier_shipping_status carrier_shipping_status { get; set; }

        public virtual ICollection<client_activity> client_activity { get; set; }
    }
}
