namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card_type
    {
        public credit_card_type()
        {
            credit_card = new HashSet<credit_card>();
        }

        [Key]
        public int credit_card_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string credit_card_type_name { get; set; }

        public virtual ICollection<credit_card> credit_card { get; set; }
    }
}
