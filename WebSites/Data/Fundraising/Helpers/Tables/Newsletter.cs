namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Newsletter")]
    public partial class Newsletter
    {
        [Key]
        public int Newsletter_ID { get; set; }

        [StringLength(255)]
        public string Referrer { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Fullname { get; set; }

        public bool? Unsubscribed { get; set; }
    }
}
