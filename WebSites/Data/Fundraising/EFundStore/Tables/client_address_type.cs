namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_address_type
    {
        [Key]
        [StringLength(2)]
        public string client_address_type_id { get; set; }

        [Required]
        [StringLength(25)]
        public string description { get; set; }
    }
}
