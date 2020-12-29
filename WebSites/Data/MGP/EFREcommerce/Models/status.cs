namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class status
    {
        public status()
        {
            catalogs = new HashSet<catalog>();
            orders = new HashSet<order>();
            order_detail = new HashSet<order_detail>();
            order_detail1 = new HashSet<order_detail>();
            products = new HashSet<product>();
        }

        [Key]
        public int status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string status_name { get; set; }

        [StringLength(50)]
        public string short_description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<catalog> catalogs { get; set; }

        public virtual ICollection<order> orders { get; set; }

        public virtual ICollection<order_detail> order_detail { get; set; }

        public virtual ICollection<order_detail> order_detail1 { get; set; }

        public virtual ICollection<product> products { get; set; }
    }
}
