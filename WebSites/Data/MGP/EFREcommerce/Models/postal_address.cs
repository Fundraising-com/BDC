namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class postal_address
    {
        public postal_address()
        {
            credit_card = new HashSet<credit_card>();
            orders = new HashSet<order>();
            shipment_group = new HashSet<shipment_group>();
        }

        [Key]
        public int postal_address_id { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(50)]
        public string address1 { get; set; }

        [StringLength(50)]
        public string address2 { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string zip { get; set; }

        [StringLength(4)]
        public string zip4 { get; set; }

        [StringLength(7)]
        public string subdivision_code { get; set; }

        public bool? residential_area { get; set; }

        public int deleted { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<credit_card> credit_card { get; set; }

        public virtual ICollection<order> orders { get; set; }

        public virtual subdivision subdivision { get; set; }

        public virtual ICollection<shipment_group> shipment_group { get; set; }
    }
}
