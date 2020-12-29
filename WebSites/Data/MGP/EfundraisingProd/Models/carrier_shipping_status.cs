namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class carrier_shipping_status
    {
        public carrier_shipping_status()
        {
            client_activity_type = new HashSet<client_activity_type>();
            sale_carrier_shipping_status = new HashSet<sale_carrier_shipping_status>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte carrier_shipping_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<client_activity_type> client_activity_type { get; set; }

        public virtual ICollection<sale_carrier_shipping_status> sale_carrier_shipping_status { get; set; }
    }
}
