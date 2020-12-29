namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class newsletter_subscription
    {
        [Key]
        public int subscription_id { get; set; }

        public int partner_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        [StringLength(120)]
        public string referrer { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string fullname { get; set; }

        public bool unsubscribed { get; set; }

        public DateTime subscribe_date { get; set; }

        public DateTime? unsubscribe_date { get; set; }
    }
}
