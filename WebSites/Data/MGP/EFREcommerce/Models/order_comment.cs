namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_comment
    {
        public order_comment()
        {
            orders = new HashSet<order>();
        }

        [Key]
        public int order_comment_id { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(100)]
        public string comment { get; set; }

        public bool visible { get; set; }

        public bool deleted { get; set; }

        public virtual ICollection<order> orders { get; set; }
    }
}
