namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class session_item
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int sessionId { get; set; }
        [Required]
        public DateTime created_on { get; set; }
        [Required]
        [StringLength(128)]
        public string name { get; set; }
        [Required]
        [StringLength(400)]
        public string value { get; set; }
    }
}
