namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("banner")]
    public partial class banner
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(400)]
        public string image { get; set; }
        [Required]
        [MaxLength(400)]
        public string url { get; set; }
        [MaxLength(400)]
        public string alternative_text { get; set; }
        [Required]
        public DateTime created_on { get; set; }
        [Required]
        public bool is_active { get; set; }
        [Required]
        public int partner_id { get; set; }
        [Required]
        public int type { get; set; }

        public int sort_order { get; set; }
    }
}
