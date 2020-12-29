namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class package_package_category
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int package_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string package_category_id { get; set; }

        public int? display_order { get; set; }

        public DateTime? create_date { get; set; }
    }
}
