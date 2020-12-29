namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("unsubscribe")]
    public partial class unsubscribe
    {
        [Key]
        public int unsubscribe_id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public bool unsubscribed { get; set; }

        public DateTime unsubscribed_date { get; set; }
    }
}
