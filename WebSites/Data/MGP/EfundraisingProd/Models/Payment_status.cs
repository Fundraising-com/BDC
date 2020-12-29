namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment_status
    {
        public Payment_status()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        public int Payment_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<payment> payments { get; set; }
    }
}
