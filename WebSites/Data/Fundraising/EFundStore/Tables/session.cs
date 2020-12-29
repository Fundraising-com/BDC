namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("session")]
    public partial class session
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string anonymous_id { get; set; }
        [Required]
        public DateTime created_on { get; set; }
    }
}
