namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("carrier")]
    public partial class carrier
    {
        public carrier()
        {
            accounting_class = new HashSet<accounting_class>();
            promotional_kit = new HashSet<promotional_kit>();
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        public byte carrier_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public byte active { get; set; }

        [StringLength(4)]
        public string SCAC { get; set; }

        public virtual ICollection<accounting_class> accounting_class { get; set; }

        public virtual ICollection<promotional_kit> promotional_kit { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
