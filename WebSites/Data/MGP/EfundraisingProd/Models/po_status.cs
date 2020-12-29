namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class po_status
    {
        public po_status()
        {
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        public byte po_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public bool? active { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
