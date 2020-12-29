namespace GA.BDC.Data.MGP.eFundweb.Models
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

        [Required]
        [StringLength(120)]
        public string Referrer { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }

        public bool? Unsubscribed { get; set; }

        public DateTime? Subscribe_Date { get; set; }

        public int Partner_ID { get; set; }
    }
}
