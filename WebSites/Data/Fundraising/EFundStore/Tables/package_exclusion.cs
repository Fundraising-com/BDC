namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("package_exclusion")]
    public partial class package_exclusion
    {
        [Key]        
        public int id { get; set; }
        [Required]
        public int package_id { get; set; }
        [Required]
        public int partner_id { get; set; }
    }
}
