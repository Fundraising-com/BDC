namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("template")]
    public partial class template
    {
        [Key]
        public int template_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(1000)]
        public string path { get; set; }

        public DateTime? create_date { get; set; }
    }
}
