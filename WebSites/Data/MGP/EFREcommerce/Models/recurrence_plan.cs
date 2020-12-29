namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class recurrence_plan
    {
        public recurrence_plan()
        {
            order_detail = new HashSet<order_detail>();
        }

        [Key]
        public int recurrence_plan_id { get; set; }

        [Required]
        [StringLength(50)]
        public string recurrence_plan_name { get; set; }

        [Required]
        [StringLength(25)]
        public string frequency { get; set; }

        public DateTime create_date { get; set; }

        public bool deleted { get; set; }

        public int? annual_frequency { get; set; }

        public virtual ICollection<order_detail> order_detail { get; set; }
    }
}
