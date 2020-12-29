namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("title")]
    public partial class title
    {
        [Key]
        public byte title_id { get; set; }

        public byte party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string title_desc { get; set; }
    }
}
