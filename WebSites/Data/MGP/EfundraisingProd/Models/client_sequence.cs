namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_sequence
    {
        public client_sequence()
        {
            clients = new HashSet<client>();
        }

        [Key]
        [StringLength(2)]
        public string client_sequence_code { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public bool is_active { get; set; }

        public virtual ICollection<client> clients { get; set; }
    }
}
