namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_partner")]
    public partial class C_tbd_partner
    {
        public C_tbd_partner()
        {
            consultants = new HashSet<consultant>();
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        public byte partner_group_type_id { get; set; }

        public byte partner_subgroup_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_name { get; set; }

        [StringLength(50)]
        public string partner_path { get; set; }

        [StringLength(150)]
        public string esubs_url { get; set; }

        [StringLength(150)]
        public string estore_url { get; set; }

        [StringLength(150)]
        public string free_kit_url { get; set; }

        [StringLength(50)]
        public string logo { get; set; }

        [StringLength(25)]
        public string phone_number { get; set; }

        [StringLength(50)]
        public string email_ext { get; set; }

        [StringLength(50)]
        public string url { get; set; }

        public Guid guid { get; set; }

        public bool prize_eligible { get; set; }

        public bool has_collection_site { get; set; }

        public virtual ICollection<consultant> consultants { get; set; }

        public virtual partner_group_types partner_group_types { get; set; }

        public virtual partner_group_types partner_group_types1 { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }
    }
}
