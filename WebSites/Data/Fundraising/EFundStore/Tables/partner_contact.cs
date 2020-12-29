namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_contact
    {
        [Key]
        public short partner_contact_id { get; set; }

        public int partner_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(50)]
        public string section_name { get; set; }

        [Required]
        [StringLength(500)]
        public string section_value { get; set; }

        public byte display_order { get; set; }
    }
}
