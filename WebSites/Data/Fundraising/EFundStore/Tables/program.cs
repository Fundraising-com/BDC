namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("program")]
    public partial class program
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int program_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public DateTime create_date { get; set; }
    }
}
