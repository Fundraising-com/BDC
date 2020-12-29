namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organization_Class
    {
        public Organization_Class()
        {
            clients = new HashSet<client>();
        }

        [Key]
        [StringLength(4)]
        public string Organization_Class_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool? Accept_PO { get; set; }

        public bool Is_Distributor { get; set; }

        public virtual ICollection<client> clients { get; set; }
    }
}
