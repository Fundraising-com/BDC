namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("newsletter")]
    public partial class newsletter
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string title { get; set; }
        
        [Required]
        [StringLength(200)]
        public string url { get; set; }
        
        [Required]
        public string body { get; set; }
        [Required]
        public DateTime created_on { get; set; }
        [Required]
        public bool enabled { get; set; }
        [Required]
        public string author { get; set; }

        public DateTime? updated_on { get; set; }

        public int? partner { get; set; }
        [Required]
        public byte display_order { get; set; }
    }
}
