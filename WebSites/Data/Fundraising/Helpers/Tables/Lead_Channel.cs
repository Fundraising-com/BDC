namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Channel
    {
        public Lead_Channel()
        {
            clients = new HashSet<client>();
            leads = new HashSet<lead>();
            Lead_Visit = new HashSet<Lead_Visit>();
            Partner_Lead_Commission = new HashSet<Partner_Lead_Commission>();
        }

        [Key]
        [StringLength(4)]
        public string Channel_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<client> clients { get; set; }

        public virtual ICollection<lead> leads { get; set; }

        public virtual ICollection<Lead_Visit> Lead_Visit { get; set; }

        public virtual ICollection<Partner_Lead_Commission> Partner_Lead_Commission { get; set; }
    }
}
