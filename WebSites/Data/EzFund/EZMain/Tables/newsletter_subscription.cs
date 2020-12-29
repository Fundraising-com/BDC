using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("newsletter_subscription")]
    public partial class newsletter_subscription
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int subscription_id { get; set; }

        public int partner_id { get; set; }

        [StringLength(120)]
        public string referrer { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string fullname { get; set; }

        public bool unsubscribed { get; set; }

        public DateTime subscribed_date { get; set; }

        public DateTime? unsubscribe_date { get; set; }

        [StringLength(50)]
        public string language_code { get; set; }
    }
}
