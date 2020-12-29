namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tell_a_friend
    {
        [Key]
        public int tell_a_friend_id { get; set; }

        [StringLength(50)]
        public string culture_code { get; set; }

        [StringLength(256)]
        public string from_name { get; set; }

        [StringLength(256)]
        public string from_email { get; set; }

        [StringLength(256)]
        public string to_name { get; set; }

        [StringLength(256)]
        public string to_email { get; set; }

        [StringLength(256)]
        public string subject { get; set; }

        [StringLength(8000)]
        public string message { get; set; }

        public bool? bounced { get; set; }

        public DateTime? datesent { get; set; }
    }
}
