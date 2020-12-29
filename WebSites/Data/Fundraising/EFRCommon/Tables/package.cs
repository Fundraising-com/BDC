namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("package")]
    public partial class package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int package_id { get; set; }

        public int? parent_package_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public byte? profit_percentage { get; set; }

        public bool enabled { get; set; }

        public DateTime? create_date { get; set; }
    }
}
