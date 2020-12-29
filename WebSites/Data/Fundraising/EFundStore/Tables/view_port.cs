namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("view_port")]
    public partial class view_port
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(40)]
        public string name { get; set; }
        [Required]
        [MaxLength(40)]
        public string bootstrap_class { get; set; }
        [Required]
        public DateTime created_on { get; set; }
    }
}
