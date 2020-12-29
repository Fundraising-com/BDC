namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("title")]
    public partial class title
    {
        public title()
        {
            clients = new HashSet<client>();
            leads = new HashSet<lead>();
            title_desc1 = new HashSet<title_desc>();
        }

        [Key]
        public byte title_id { get; set; }

        public byte party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string title_desc { get; set; }

        public virtual ICollection<client> clients { get; set; }

        public virtual ICollection<lead> leads { get; set; }

        public virtual party_type party_type { get; set; }

        public virtual ICollection<title_desc> title_desc1 { get; set; }
    }
}
