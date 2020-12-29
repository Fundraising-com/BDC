namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_type_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte group_type_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public virtual group_type group_type { get; set; }

        public virtual language language { get; set; }
    }
}
