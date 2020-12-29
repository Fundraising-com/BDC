namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_address_type
    {
        public client_address_type()
        {
            client_address = new HashSet<client_address>();
        }

        [Key]
        [StringLength(2)]
        public string address_type { get; set; }

        [Required]
        [StringLength(25)]
        public string address_type_desc { get; set; }

        public virtual ICollection<client_address> client_address { get; set; }
    }
}
