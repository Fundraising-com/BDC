namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_type_name { get; set; }

        public DateTime create_date { get; set; }
    }
}
