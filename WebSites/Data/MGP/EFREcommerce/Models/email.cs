namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("email")]
    public partial class email
    {
        public email()
        {
            orders = new HashSet<order>();
            shipment_group = new HashSet<shipment_group>();
        }

        [Key]
        public int email_id { get; set; }

        [Required]
        [StringLength(320)]
        public string email_address { get; set; }

        [Required]
        [StringLength(50)]
        public string recipient_name { get; set; }

        public int deleted { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<order> orders { get; set; }

        public virtual ICollection<shipment_group> shipment_group { get; set; }
    }
}
