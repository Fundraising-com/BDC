namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_group_types
    {
        public partner_group_types()
        {
            C_tbd_partner = new HashSet<C_tbd_partner>();
            C_tbd_partner1 = new HashSet<C_tbd_partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte partner_group_type_id { get; set; }

        [Required]
        [StringLength(20)]
        public string partner_group_type_desc { get; set; }

        public virtual ICollection<C_tbd_partner> C_tbd_partner { get; set; }

        public virtual ICollection<C_tbd_partner> C_tbd_partner1 { get; set; }
    }
}
