namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("language")]
    public partial class language
    {
        [Key]
        [StringLength(2)]
        public string language_code { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
    }
}
