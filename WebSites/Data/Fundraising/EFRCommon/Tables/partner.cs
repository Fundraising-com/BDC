namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("partner")]
    public partial class partner
    {
        [Key]
        public int partner_id { get; set; }

        public int partner_type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string partner_name { get; set; }

        public bool has_collection_site { get; set; }

        public Guid guid { get; set; }

        public DateTime create_date { get; set; }

        public bool is_active { get; set; }
    }
}
