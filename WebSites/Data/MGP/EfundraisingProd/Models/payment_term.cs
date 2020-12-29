namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_term
    {
        public payment_term()
        {
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        public byte payment_term_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public decimal discount_percent { get; set; }

        public short? lead_days { get; set; }

        public int? default_ar_status { get; set; }

        public bool hide_from_consultants { get; set; }

        public int? ext_payment_type_id { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
