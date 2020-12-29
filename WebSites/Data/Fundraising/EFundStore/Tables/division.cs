namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("division")]
    public partial class division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int division_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        public string logo { get; set; }

        [StringLength(10)]
        public string short_name { get; set; }
    }
}
