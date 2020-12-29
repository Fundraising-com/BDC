namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("source")]
    public partial class source
    {
        public source()
        {
            orders = new HashSet<order>();
        }

        [Key]
        public int source_id { get; set; }

        [Required]
        [StringLength(50)]
        public string source_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<order> orders { get; set; }
    }
}
