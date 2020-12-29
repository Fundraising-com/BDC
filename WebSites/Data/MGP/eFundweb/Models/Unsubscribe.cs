namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unsubscribe")]
    public partial class Unsubscribe
    {
        [Key]
        public int Unsubscribe_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public bool Unsubscribed { get; set; }

        public DateTime Unsubscribed_Date { get; set; }
    }
}
