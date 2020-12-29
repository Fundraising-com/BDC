namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("division")]
    public partial class division
    {
        public division()
        {
            advertisements = new HashSet<advertisement>();
            clients = new HashSet<client>();
            consultants = new HashSet<consultant>();
            leads = new HashSet<lead>();
        }

        [Key]
        public byte division_id { get; set; }

        [Required]
        [StringLength(50)]
        public string division_name { get; set; }

        [StringLength(50)]
        public string logo { get; set; }

        [Required]
        [StringLength(10)]
        public string short_name { get; set; }

        public virtual ICollection<advertisement> advertisements { get; set; }

        public virtual ICollection<client> clients { get; set; }

        public virtual ICollection<consultant> consultants { get; set; }

        public virtual ICollection<lead> leads { get; set; }
    }
}
