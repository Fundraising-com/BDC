namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("territory")]
    public partial class territory
    {
        public territory()
        {
            consultants = new HashSet<consultant>();
        }

        [Key]
        public short territory_id { get; set; }

        [Required]
        [StringLength(25)]
        public string territory_name { get; set; }

        public virtual ICollection<consultant> consultants { get; set; }
    }
}
