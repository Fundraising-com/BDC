namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class phone_number
    {
        public phone_number()
        {
            credit_card = new HashSet<credit_card>();
            orders = new HashSet<order>();
            shipment_group = new HashSet<shipment_group>();
        }

        [Key]
        public int phone_number_id { get; set; }

        [Column("phone_number")]
        [Required]
        [StringLength(20)]
        public string phone_number1 { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<credit_card> credit_card { get; set; }

        public virtual ICollection<order> orders { get; set; }

        public virtual ICollection<shipment_group> shipment_group { get; set; }
    }
}
