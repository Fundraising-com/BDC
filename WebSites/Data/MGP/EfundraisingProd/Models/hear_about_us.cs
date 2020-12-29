namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class hear_about_us
    {
        public hear_about_us()
        {
            hear_about_us_desc = new HashSet<hear_about_us_desc>();
            leads = new HashSet<lead>();
        }

        [Key]
        public byte hear_id { get; set; }

        public byte party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public byte order_on_web { get; set; }

        public bool is_active { get; set; }

        public virtual ICollection<hear_about_us_desc> hear_about_us_desc { get; set; }

        public virtual party_type party_type { get; set; }

        public virtual ICollection<lead> leads { get; set; }
    }
}
