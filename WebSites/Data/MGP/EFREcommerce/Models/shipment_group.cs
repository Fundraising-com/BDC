namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shipment_group
    {
        public shipment_group()
        {
            order_detail = new HashSet<order_detail>();
        }

        [Key]
        public int shipment_group_id { get; set; }

        public int? shipping_postal_address_id { get; set; }

        public int? shipping_phone_number_id { get; set; }

        public int? shipping_email_id { get; set; }

        public DateTime? shipment_date { get; set; }

        [Column(TypeName = "money")]
        public decimal shipping_charges { get; set; }

        public bool deleted { get; set; }

        [StringLength(15)]
        public string fulf_shipment_tracking { get; set; }

        public virtual email email { get; set; }

        public virtual ICollection<order_detail> order_detail { get; set; }

        public virtual phone_number phone_number { get; set; }

        public virtual postal_address postal_address { get; set; }
    }
}
